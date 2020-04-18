using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace RandomUserCore.Models.IntegrationModels
{
    public class CommonModel
    {
         [ReadOnly(true)]
        [Description("A universally unique ID")]
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [ReadOnly(true)]
        [Description("The time at which this Model was created")]
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [ReadOnly(true)]
        [Description("The last time at which this Model was updated")]
        public DateTime? UpdatedAt { get; set; }
    }
}