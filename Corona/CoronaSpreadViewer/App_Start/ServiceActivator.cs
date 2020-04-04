using Backend.DependencyInjection;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace CoronaSpreadViewer.App_Start
{
    public class ServiceActivator : IHttpControllerActivator
    {
        public Container Container { get; }

        public ServiceActivator() {
            Container = new Container();
        }

        public IHttpController Create(HttpRequestMessage request
            , HttpControllerDescriptor controllerDescriptor, Type controllerType) {
            var controller = Container.GetInstance(controllerType) as IHttpController;
            return controller;
        }
    }
}