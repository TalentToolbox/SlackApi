using Newtonsoft.Json;
using System;

namespace SlackConnection.Responses
{
    public abstract class Response
    {
        /// <summary>
        /// Should always be checked before trying to process a response.
        /// </summary>
        [JsonProperty(PropertyName = "ok")]
        public bool Ok;

        /// <summary>
        /// if ok is false, then this is the reason-code
        /// </summary>
        [JsonProperty(PropertyName = "error")]
        public string Error;
        [JsonProperty(PropertyName = "needed")]
        public string Needed;
        [JsonProperty(PropertyName = "provided")]
        public string Provided;

        public void AssertOk()
        {
            if (!Ok)
                throw new InvalidOperationException($"An error occurred: {Error}");
        }

        [JsonProperty(PropertyName = "response_metadata")]
        public ResponseMetaData ResponseMetadata;
    }

    public class ResponseMetaData
    {
        [JsonProperty(PropertyName = "next_cursor")]
        public string NextCursor;
    }
}
