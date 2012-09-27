(function (window, $) {
    'use strict';

    toastr.options = {
        positionClass: 'toast-bottom-right',
        timeOut: 10000
    }

    $("form[action*='Todo/MarkComplete'] :checkbox").on("change", function (event) {
        var self = $(this);
        var checked = self.prop('checked');
        console.log(checked);

        $("form[action*='Todo/MarkComplete']").ajaxSubmit({
            data: { complete: checked },
            success: function (data) {
                toastr.info("Not Implemented", "Completed Action Captured<br />" + data.response);
                //window.log(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                toastr.error(errorThrown, "Completed Action Captured");
                //window.log(arguments);
            }
        });
    });

    $("form[action*='Todo/Delete']").on("submit", function (event) {
        event.preventDefault();
        $(this).ajaxSubmit({
            success: function (data) {
                toastr.info("Not Implemented", "Delete Action Captured<br />" + data.response);
                //window.log(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                toastr.error(errorThrown, "Delete Action Captured");
                //window.log(arguments);
            }
        });
    });

    $("#form_Create").on("submit", function (event) {
        event.preventDefault();
        var self = $(this);
        if (!self.find('#new-todo').val()) {
            toastr.error("Invalid Data", "Create New Todo");
            return;
        }

        $(this).ajaxSubmit({
            success: function (data) {
                toastr.success("Saved to database", "Create New Todo");
                $('#new-todo').val('');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                toastr.error(errorThrown, "Create New Action Captured");
                //window.log(arguments);
            },
            target: '#todo-list'
        });
    });

})(window, jQuery);
