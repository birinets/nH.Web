ko.bindingHandlers.scroll = {
	init: function (elem, valueAccessor) {
		window.setTimeout(function() {
			var floor = $(document).height() + 100;
			$('html, body').scrollTop(floor);
		}, 1);
	}
};

ko.bindingHandlers.date = {
	init: function (elem, valueAccessor) {
		var value = valueAccessor();
		var valueUnwrapped = new Date(ko.utils.unwrapObservable(value));
		$(elem).text(valueUnwrapped.toLocaleString());
	}
}

ko.bindingHandlers.commitText = {
	init: function (elem, valueAccessor) {
		var value = valueAccessor();
		var valueUnwrapped = ko.utils.unwrapObservable(value);
		if (valueUnwrapped.length) {
			$(elem).text(valueUnwrapped.substr(0, 7) + '...');
		}
	}
}