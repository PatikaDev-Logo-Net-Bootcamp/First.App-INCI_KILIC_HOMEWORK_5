using First.App.Business.Abstract;
using First.App.Domain.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient httpClient;
        private readonly IPostService postService;
       

        public Worker(ILogger<Worker> logger, IPostService postService)
        {
            _logger = logger;
            this.postService = postService;
           
        }

        //worker'ý ayaða kaldýran metot
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            httpClient = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        //iþlemi durdurmak için kullanýlýyor
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            httpClient.Dispose();
            return base.StopAsync(cancellationToken);
        }

        // iþlem bittikten sonra worker'ý garbage collector'dan silmek için kullanýyorum
        public override void Dispose()
        {
            base.Dispose();
        }

        //execute iþlemleri burada yapýlýyor.
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // içerikte dönen bilgileri string olarak okuyor
                httpClient.BaseAddress = new Uri("http://jsonplaceholder.typicode.com/");
                var posts = await httpClient.GetFromJsonAsync<Post[]>("posts");

                foreach( var item in posts)
                {
                    postService.AddPost(item);
                }

                await Task.Delay(60000, stoppingToken);
                // worker hangi aralýklarla çalýþacak onu ayarlýyorum
            }
        }
    }
}
