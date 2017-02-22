namespace WpfSampleUI
{
    using System;
    using System.Net;

    using Newtonsoft.Json;

    using RestSharp;
    using RestSharp.Deserializers;

    public class CBSClient
    {
        private const string BookUrl = "vehicle/book";
        private const string FinalizeUrl = "vehicle/finalize";

        public int MakeReservation(MakeReservationDto makeReservationDto)
        {
            var serializedReservation = JsonConvert.SerializeObject(makeReservationDto);
            var request = new RestRequest(BookUrl, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddParameter("application/json", serializedReservation, ParameterType.RequestBody);
            return this.Execute<MakeReservationResponseDto>(request).ReservationId;
        }

        public UpdateReservationResponseDto UpdateReservation(UpdateReservationDto updateReservationDto)
        {
            var serializedReservation = JsonConvert.SerializeObject(updateReservationDto);
            var request = new RestRequest(FinalizeUrl, Method.PATCH) { RequestFormat = DataFormat.Json };
            request.AddParameter("application/json", serializedReservation, ParameterType.RequestBody);
            return this.Execute<UpdateReservationResponseDto>(request);
        }

        private T Execute<T>(RestRequest request) where T : new()
        {
            var client = this.GetClient();
            var response = client.Execute(request);
            var deserializer = new JsonDeserializer();
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {
                return deserializer.Deserialize<T>(response);
            }

            throw new Exception(response.Content);
        }

        private RestClient GetClient()
        {
            return new RestClient("http://localhost:8080");
        }
    }
}
