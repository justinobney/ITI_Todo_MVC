(function (window, $, toastr) {
    'use strict';

    toastr.options = {
        positionClass: 'toast-bottom-right',
        timeOut: 10000
    };

    var Todo_Actions = {
        Create: function (_form) {
            var self = $(_form);
            if (!self.find('#new-todo').val()) {
                toastr.error("Invalid Data", "Create New Todo");
                return;
            }

            self.ajaxSubmit({
                success: function () {
                    toastr.success("Saved to database", "Create New Todo");
                    $('#new-todo').val('');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    toastr.error(errorThrown, "Create New Action Captured");
                },
                target: '#todo-list'
            });
        },
        MarkComplete: function (_form) {
            $(_form).ajaxSubmit({
                success: function () {
                    $('#new-todo').focus();
                     toastr.success("Saved to database");
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    toastr.error(errorThrown, "Completed Action Captured");
                },
                target: '#todo-list'
            });
        },
        Delete: function (_form) {
            if(confirm("Are you sure you want to delete this?")){
                return false;
            }
            
            $(_form).ajaxSubmit({
                success: function () {
                    $('#new-todo').focus();
                    toastr.success("Deleted from database");
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    toastr.error(errorThrown, "Delete Action Captured");
                },
                target: '#todo-list'
            });
        }
    };

    window.trigger_checkbox_change = function (el) {
        $(el).closest('form').trigger('submit');
    };

    //Setup URL Hash Change Events. Refactor this into the requireJS format...
    $.getScript('https://raw.github.com/Nijikokun/Hasher/master/hasher.min.js', function(){
        var todo_list = $('#todo-list');

        Hasher.add("/active", function () {
            todo_list.find('li')
                .show();

            todo_list.find('li.completed')
                .hide();
        });

        Hasher.add("/completed", function () {
            todo_list.find('li')
                .hide();

            todo_list.find('li.completed')
                .show();
        });

        Hasher.add("/", function () {
            todo_list.find('li')
                .show();
        });

        // Setup the Hasher
        Hasher.setup();
    });

    $("body").on("submit", function (event) {
        switch (true) {
            case /Todo\/Create/.test(event.target.action):
                event.preventDefault();
                //console.log("• Matched 'Create' test");
                Todo_Actions.Create(event.target);
                break;
            case /Todo\/MarkComplete/.test(event.target.action):
                event.preventDefault();
                //console.log("• Matched 'MarkComplete' test");
                Todo_Actions.MarkComplete(event.target);
                break;
            case /Todo\/Delete/.test(event.target.action):
                event.preventDefault();
                //console.log("• Matched 'Delete' test");
                Todo_Actions.Delete(event.target);
                break;
            default:
                console.log("• Didn't match any test");
                break;
        }
    });
})(window, jQuery, toastr);