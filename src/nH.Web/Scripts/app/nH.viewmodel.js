window.nHApp.nHViewModel = (function (ko, datacontext) {
	var list = ko.observableArray(),
		error = ko.observable();

	datacontext.getList(list, error);

	return {
		list: list,
		update: update
	};

	function update() {
		datacontext.getList(list, error);
		window.setTimeout(update, 2000);
	}
})(ko, nHApp.datacontext);

window.setTimeout(window.nHApp.nHViewModel.update, 2000);
ko.applyBindings(window.nHApp.nHViewModel);