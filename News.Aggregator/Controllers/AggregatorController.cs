namespace News.Aggregator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AggregatorController:ControllerBase
	{
		private readonly INewsServices _news;

		public AggregatorController(INewsServices news)
		{
			_news = news;
		}

		[HttpPost]
		[Route("PostActionContollerName")]
		public Task PostActionContollerName()
		{
			return _news.PostActionContollerName();
		}
	}
}
