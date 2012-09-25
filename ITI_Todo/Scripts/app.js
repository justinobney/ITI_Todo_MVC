(function( window, $ ) {
	'use strict';

	toastr.options = {
	    positionClass: 'toast-bottom-right',
	    timeOut: 10000
	}

	$("form[action*='Todo/MarkComplete'] :checkbox").on("change", function (event) {
	    toastr.info("Not Implemented", "Completed Action Captured");
	});

	$("form[action*='Todo/Delete']").on("submit", function (event) {
	    event.preventDefault();

	    toastr.error("Not Implemented", "Delete Action Captured");
	});

	$("#form_Create").on("submit", function (event) {
	    event.preventDefault();
	    $(this).ajaxSubmit({
	        success: function (data) {
	            toastr.success("Not Implemented", "Create New Action Captured");
	            //window.log(data);
	        },
	        error: function (jqXHR, textStatus, errorThrown) {
	            toastr.error(errorThrown, "Create New Action Captured");
	            //window.log(arguments);
	        }
	    });
	});

})( window, jQuery );
