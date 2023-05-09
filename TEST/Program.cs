using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Xunit;
using Autofac;
using Web_Api_CRUD.Model;
using TEST.Infraestructure;

namespace TEST
{
   public class Program
    {
        // public static void Main(string[] args)
        // {
        //     var builder = Host.CreateDefaultBuilder(args)
        //         .ConfigureServices(services =>
        //         {
        //             var containerBuilder = new ContainerBuilder();
        //             containerBuilder.RegisterModule(new AutoFacModel());
        //             containerBuilder.RegisterModule(new AutofacInfraestructure());

        //         });
        //     var host = builder.Build();
        // }
    }
}