ko.bindingHandlers.scroll = {
	init: function (elem, valueAccessor) {
		window.setTimeout(function() {
			var floor = $(document).height() + 100;
			$('html, body').scrollTop(floor);
		}, 1);
	}
};