window.nHApp.nHViewModel = (function (ko, datacontext) {
	var list = ko.observableArray(),
		error = ko.observable();

	datacontext.getList(list, error);

	return {
		list: list,
		update: update
	};

	function update() {
		datacontext.getExtList(list, error);
		window.setTimeout(update, window.nHApp.interval);
	}
})(ko, nHApp.datacontext);

window.setTimeout(window.nHApp.nHViewModel.update, window.nHApp.interval);
ko.applyBindings(window.nHApp.nHViewModel);