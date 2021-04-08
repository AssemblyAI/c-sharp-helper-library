using System.Collections.Generic;
using System.Text.Json;
using AssemblyAi.Common.Dtos;
using AssemblyAi.Common.Dtos.RequestModels;
using AssemblyAi.Common.Enums;
using AssemblyAi.Common.Helpers;

namespace AssemblyAiUnitTests
{
    public static class TestModels
    {
        public static AssemblyAiAccount Account = new AssemblyAiAccount {AuthToken = "my auth token"};

        public static TranscriptionRequest SubmitTranscriptionRequest = new TranscriptionRequest
        {
            AudioUrl = "https://s3-us-west-2.amazonaws.com/blog.assemblyai.com/audio/8-7-2018-post/7510.mp3"
        };

        public static TranscriptionResponse SubmitTranscriptionResponse = new TranscriptionResponse
        {
            Id = "5551722-f677-48a6-9287-39c0aafd9ac1",

            Status = "queued",
            AcousticModel = AcousticModelEnum.Default,
            AudioDuration = null,
            AudioUrl = "https://s3-us-west-2.amazonaws.com/blog.assemblyai.com/audio/8-7-2018-post/7510.mp3",
            Confidence = null,
            DualChannel = null,
            FormatText = true,
            LanguageModel = AcousticModelEnum.Default,
            Punctuate = true,
            Text = null,
            Utterances = null,
            WebhookStatusCode = null,
            WebhookUrl = null,
            Words = null
        };

        public static string TranscriptionId = "5551722-f677-48a6-9287-39c0aafd9ac1";

        public static string SubmitJsonResponse = @"{
							""id"": ""5551722-f677-48a6-9287-39c0aafd9ac1"",
							""status"": ""queued"",
							""acoustic_model"": ""assemblyai_default"",
							""audio_duration"": null,
							""audio_url"": ""https://s3-us-west-2.amazonaws.com/blog.assemblyai.com/audio/8-7-2018-post/7510.mp3"",
							""confidence"": null,
							""dual_channel"": null,
							""format_text"": true,
							""language_model"": ""assemblyai_default"",
							""punctuate"": true,
							""text"": null,
							""utterances"": null,
							""webhook_status_code"": null,
							""webhook_url"": null,
							""words"": null
							}";


        public static TranscriptionResponse RetrieveTranscriptionResponse = new TranscriptionResponse
        {
            Id = "5551722-f677-48a6-9287-39c0aafd9ac1",
            Status = "completed",
            AcousticModel = AcousticModelEnum.Default,
            AudioDuration = 12.0960090702948,
            AudioUrl = "https://s3-us-west-2.amazonaws.com/blog.assemblyai.com/audio/8-7-2018-post/7510.mp3",
            Confidence = 0.956,
            DualChannel = null,
            FormatText = true,
            LanguageModel = AcousticModelEnum.Default,
            Punctuate = true,
            Text =
                "You know Demons on TV like that and and for people to expose themselves to being rejected on TV or humiliated by fear factor or.",
            Utterances = null,
            WebhookStatusCode = null,
            WebhookUrl = null,
            Words = new List<Words>
            {
                new Words
                {
                    Confidence = 1.0,
                    End = 440,
                    Start = 0,
                    Text = "You"
                }
            }
        };

      
        
        public static string RetrieveJsonResult = @"{
				    ""acoustic_model"": ""assemblyai_default"",
					        ""audio_duration"": 12.0960090702948,
					        ""audio_url"": ""https://s3-us-west-2.amazonaws.com/blog.assemblyai.com/audio/8-7-2018-post/7510.mp3"",
					        ""confidence"": 0.956,
					        ""dual_channel"": null,
					        ""format_text"": true,
					        ""id"": ""5551722-f677-48a6-9287-39c0aafd9ac1"",
					        ""language_model"": ""assemblyai_default"",
					        ""punctuate"": true,
					        ""status"": ""completed"",
					        ""text"": ""You know Demons on TV like that and and for people to expose themselves to being rejected on TV or humiliated by fear factor or."",
					        ""utterances"": null,
					        ""webhook_status_code"": null,
					        ""webhook_url"": null,
					        ""words"": [
					        {
						        ""confidence"": 1.0,
						        ""end"": 440,
						        ""start"": 0,
						        ""text"": ""You""
					        }
					        ]
				        }";


        public static JsonSerializerOptions EnumJsonSerializeOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new EnumConvertor<AcousticModelEnum>(),
                new EnumConvertor<BoostParamEnum>()
            }
        };
    }
}