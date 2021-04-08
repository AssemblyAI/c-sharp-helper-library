using AssemblyAi;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using AssemblyAi.Common.Dtos;
using AssemblyAi.Common.Dtos.RequestModels;
using AssemblyAi.Common.Enums;

namespace Consumer
{
	public class Demo
	{
		private readonly AssemblyAi.AssemblyAiService _apiService;

		public Demo(AssemblyAi.AssemblyAiService apiService)
		{
			_apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
		}

		public async Task<TranscriptionResponse> UploadTranscription()
		{
			var request = new TranscriptionRequest
			{
				AudioUrl = "https://s3-us-west-2.amazonaws.com/blog.assemblyai.com/audio/8-7-2018-post/7510.mp3",
				AcousticModelEnum = AcousticModelEnum.UnitedKingdom
			};
			return await _apiService
				.SubmitAudioFileAsync(request);

		}

		public async Task<TranscriptionResponse> RetrieveTranscription(string id)
		{
			return await _apiService.RetrieveAudioFileAsync(id);
		}

	}
}
