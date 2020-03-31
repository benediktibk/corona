using Backend;
using Backend.Service;
using ScalableVectorGraphic;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CoronaSpreadViewer.Controllers
{
    public class GraphController : ApiController
    {
        private readonly IGraphService _graphService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public GraphController(IUnitOfWorkFactory unitOfWorkFactory, IGraphService graphService) {
            _unitOfWorkFactory = unitOfWorkFactory;
            _graphService = graphService;
        }

        [HttpGet]
        public HttpResponseMessage Get(int id) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                var result = _graphService.CreateGraph(unitOfWork, (GraphType)id, new List<CountryAndColor> {
                new CountryAndColor {
                    Country = CountryType.Austria,
                    Color = Color.Red
                },
                new CountryAndColor {
                    Country = CountryType.Italy,
                    Color = Color.Blue
                }
            });

                if (string.IsNullOrEmpty(result)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(result);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/svg+xml");
                response.Content.Headers.ContentEncoding.Add("utf-8");
                return response;
            }
        }
    }
}
