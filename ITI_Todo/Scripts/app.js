(function (window, $) {
    'use strict';

    toastr.options = {
        positionClass: 'toast-bottom-right',
        timeOut: 10000
    };

    var Todo_Actions = {
        Create: function(_form){
            var self = $(_form);
            if (!self.find('#new-todo').val()) {
                toastr.error("Invalid Data", "Create New Todo");
                return;
            }

            self.ajaxSubmit({
                success: function (data) {
                    toastr.success("Saved to database", "Create New Todo");
                    $('#new-todo').val('');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    toastr.error(errorThrown, "Create New Action Captured");
                },
                target: '#todo-list'
            });
        },
        MarkComplete: function(_form){
            $(_form).ajaxSubmit({
                success: function (data) {
                    toastr.info("Not Implemented", "Completed Action Captured<br />" + data.response);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    toastr.error(errorThrown, "Completed Action Captured");
                }
            });
        },
        Delete: function(_form){
            $(_form).ajaxSubmit({
                success: function (data) {
                    toastr.info("Not Implemented", "Delete Action Captured<br />" + data.response);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    toastr.error(errorThrown, "Delete Action Captured");
                }
            });
        }
    };

    $("form[action*='Todo/MarkComplete'] :checkbox").on("change", function (event) {
        $(this).closest('form').trigger('submit');
    });

    $("body").on("submit", function (event) {
        switch (true) {
          case /Todo\/Create/.test(event.target.action):
            event.preventDefault();
            console.log("• Matched 'Create' test");
            //Todo_Actions.Create(event.target);
            break;
          case /Todo\/MarkComplete/.test(event.target.action):
            event.preventDefault();
            console.log("• Matched 'MarkComplete' test");
            //Todo_Actions.MarkComplete(event.target);
            break;
          case /Todo\/Delete/.test(event.target.action):
            event.preventDefault();
            console.log("• Matched 'Delete' test");
            //Todo_Actions.Delete(event.target);
            break;
          default:
            console.log("• Didn't match any test");
            break;
        }
    });
})(window, jQuery);
