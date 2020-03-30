using Backend.DependencyInjection;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace CoronaSpreadViewer.App_Start
{
    public class ServiceActivator : IHttpControllerActivator
    {
        private readonly Container _container;

        public ServiceActivator(HttpConfiguration configuration) {
            _container = new Container();
        }

        public IHttpController Create(HttpRequestMessage request
            , HttpControllerDescriptor controllerDescriptor, Type controllerType) {
            var controller = _container.GetInstance(controllerType) as IHttpController;
            return controller;
        }
    }
}