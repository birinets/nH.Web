(function (ko, datacontext) {
	datacontext.listModel = listModel;

	function listModel(data) {
		var self = this;
		data = data || {};

		self.entryId = data.id;
		self.repo = data.repoName;
		self.message = data.message;
		self.created = data.created;
		self.commitId = data.commitId;

		self.toJson = function () { return ko.toJSON(self); };
	};
})(ko, nHApp.datacontext);