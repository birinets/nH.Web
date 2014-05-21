window.nHApp = window.nHApp || {
	interval: 10000
};

window.nHApp.datacontext = (function () {

	var datacontext = {
		getList: getList,
		getExtList: getExtList
	};

	return datacontext;

	function getList(listObservable, errorObservable) {
		return ajaxRequest("get", listUrl())
			.done(getSucceeded)
			.fail(getFailed);

		function getSucceeded(data) {
			var mappedList = $.map(data, function (list) { return new createList(list); });
			listObservable(mappedList);
		}

		function getFailed() {
			errorObservable("Error retrieving list.");
		}
	}

	function getExtList(listObservable, errorObservable) {
		var lastTime = undefined;

		var koList = listObservable();
		if (koList.length) {
			lastTime = koList[koList.length - 1].created;
		}

		return ajaxRequest("get", listExtUrl(lastTime))
			.done(getSucceeded)
			.fail(getFailed);

		function getSucceeded(data) {
			var mappedList = $.map(data, function (list) { return new createList(list); });
			$.each(mappedList, function () {
				listObservable.push(this);
			});
		}

		function getFailed() {
			errorObservable("Error retrieving list.");
		}
	}

	function createList(data) {
		return new datacontext.listModel(data);
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
			};
		}
		return $.ajax(url, options);
	}

	function listUrl(id) { return "/api/nH/" + (id || ""); }
	function listExtUrl(date) {
		var url = "/api/nH/";
		if (date) {
			url += "?date=" + date;
		}
		return url;
	}
})();