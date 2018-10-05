/*******************************************************

MIT License

Copyright (c) 2018 Matelab Srl

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

********************************************************/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Xenioo.Channels.API.Utils {
	public static class REST {

		public static R Post<T,R>( string url, T payload, Dictionary<string,string> headers  ) {
			string ret = ExecuteMethodCall( "POST", url, JsonConvert.SerializeObject( payload ), headers  );
			return JsonConvert.DeserializeObject<R>( ret );
		}

		public static T Get<T>( string url, Dictionary<string, string> headers ) {
			string ret = ExecuteMethodCall( "GET", url, null, headers  );
			return JsonConvert.DeserializeObject<T>( ret );
		}

		#region Call

		internal static string ExecuteMethodCall( string method, string url, string payload, Dictionary<string, string> headers ) {

			WebRequest request = WebRequest.Create(url);
			((HttpWebRequest) request).ServicePoint.Expect100Continue = false;
			request.Method = method;

			request.ContentType = "application/json";
			
			if( headers != null ) {
				foreach( var key in headers.Keys ) {

					string val = headers[key].Replace("\r", string.Empty)
											   .Replace("\n", string.Empty)
											   .Replace("\t", string.Empty);

					switch( key.ToLower( ) ) {
						case "content-type":
							request.ContentType = val;
							break;
						case "accept":
							((HttpWebRequest) request).Accept = val;
							break;
						case "user-agent":
							((HttpWebRequest) request).UserAgent = val;
							break;
						default:
							request.Headers.Add( key, val );
							break;
					}

				}
			}

			Stream datastream = null;

			if( payload.HasValue( ) ) {
				byte[] arr = Encoding.UTF8.GetBytes(payload);
				//request.ContentLength = arr.Length;
				datastream = request.GetRequestStream( );
				datastream.Write( arr, 0, arr.Length );
				datastream.Close( );
			}

			try {

				WebResponse response = request.GetResponse();

				datastream = response.GetResponseStream( );
				StreamReader reader = new StreamReader(datastream);
				string srvresponse = reader.ReadToEnd();
				reader.Close( );
				datastream.Close( );
				response.Close( );

				return srvresponse;

			} catch( WebException webex ) {
				using( var stream = webex.Response.GetResponseStream( ) ) {
					using( var reader = new StreamReader( stream ) ) {
						throw new Exception( webex.Message + " -> " + reader.ReadToEnd( ) );
					}
				}
			} catch( Exception ex ) {
				throw ex;
			}
		}

		#endregion

	}
}
