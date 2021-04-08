using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using AssemblyAi;
using AssemblyAi.Common.Dtos;

namespace Consumer
{
	public class Program
	{
		public async static Task Main(string[] args)
		{
			IConfiguration Configuration = new ConfigurationBuilder()
			   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
			   .AddEnvironmentVariables()
			   .AddUserSecrets<Program>()
			   .AddCommandLine(args)
			   .Build();

			
			IServiceCollection services = new ServiceCollection();

			services.AddAssemblyAi(options => {
				Configuration.GetSection("AssemblyAiAccount").Bind(options);
			});
			
			services.AddSingleton<Demo>();
			var serviceProvider = services.BuildServiceProvider();

			Demo demo = serviceProvider.GetService<Demo>();

			TranscriptionResponse result = await demo.UploadTranscription();

			TranscriptionResponse pollResult;

			do {
				pollResult = await demo.RetrieveTranscription(result.Id); 
				Console.WriteLine(pollResult.Status + " and id: " + pollResult.Id);
				Thread.Sleep(10000);
			}
			while (pollResult.Status != "completed");

			Console.WriteLine(pollResult.Status + " and Text: " + pollResult.Text);


		}


	}
}
