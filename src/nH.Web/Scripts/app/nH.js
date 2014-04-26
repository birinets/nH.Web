window.nHApp = window.nHApp || { };

window.nHApp.datacontext = (function () {

	var datacontext = {
		getList: getList
	};

	return datacontext;

	function getList(listObservable, errorObservable) {
		return ajaxRequest("get", listUrl())
			.done(getSucceeded)
			.fail(getFailed);

		function getSucceeded(data) {
			$.each(data, function() {
				listObservable.push(this);
			});
		}

		function getFailed() {
			errorObservable("Error retrieving list.");
		}
	}

	function ajaxRequest(type, url, data, dataType) {
		var options = {
			dataType: dataType || "json",
			contentType: "application/json",
			cache: false,
			type: type,
			data: data ? data.toJson() : null
		};
		var antiForgeryToken = $("#antiForgeryToken").val();
		if (antiForgeryToken) {
			options.headers = {
				'RequestVerificationToken': antiForgeryToken
			}
		}
		return $.ajax(url, options);
	}

	function listUrl(id) { return "/api/nH/" + (id || ""); }
})();

window.nHApp.nHViewModel = (function (ko, datacontext) {
	var list = ko.observableArray(),
		error = ko.observable();

	datacontext.getList(list, error);

	return {
		list: list,
		update: update
	};

	function update() {
		console.log(datacontext);
		datacontext.getList(list, error);
		window.setTimeout(update, 2000);
	}
})(ko, nHApp.datacontext);

window.setTimeout(window.nHApp.nHViewModel.update, 2000);
ko.applyBindings(window.nHApp.nHViewModel);