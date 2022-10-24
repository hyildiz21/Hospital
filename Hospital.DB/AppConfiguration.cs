using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DB
{
    public class AppConfiguration
    {
        //constructor yapım, oluşturduğumuz DB nin özellikleri, gidiş yolunu nasıl erişebileceğini yazacağız
        public AppConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();

            //dosya yolu verceksek bulmak istersek -path classı-
            //GetCurrentDirectory() projenin başladığı yeri söyler.
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            //json dosyaları=text dosyları.
            configBuilder.AddJsonFile(path,false);

            //appsetting dosyasını oku
            var root = configBuilder.Build();

            //Dbye bağlanması için appsettingsi atayacağız
            var appSetting = root.GetSection("ConnectionStrings:DefaultConnection");

            ConnectionString = appSetting.Value;

        }

        public string ConnectionString { get; set; }

    }
}
