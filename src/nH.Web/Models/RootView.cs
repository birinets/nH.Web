using System;

namespace nH.Web.Models
{
	public class RootView
	{
		public Guid Id { get; set; }
		public string RepoName { get; set; }
		public string Message { get; set; }
		public DateTime Created { get; set; }
		public string CommitId { get; set; }
	}
}