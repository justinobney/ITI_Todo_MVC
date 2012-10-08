/*global window, toastr, jQuery, confirm, Hasher, console*/
(function (window, $, toastr) {
    'use strict';

    toastr.options = {
        positionClass: 'toast-bottom-right',
        timeOut: 6000
    };

    var Todo_Actions = {
        Create: function (form) {
            var self = $(form);
            if (!self.find('#new-todo').val()) {
                toastr.error("Invalid Data", "Create New Todo");
                return;
            }

            self.ajaxSubmit({
                success: function () {
                    toastr.success("Saved to database", "Create New Todo");
                    $('#new-todo').val('');
                    $("#todoapp").triggerHandler("db_action_complete");
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    toastr.error(errorThrown, "Create New Action Captured");
                },
                target: '#todo-list'
            });
        },
        Update: function (form) {
            var self = $(form);
            if (!self.find('[name="todo"]').val()) {
                toastr.error("Invalid Data", "Update Todo");
                return;
            }

            self.ajaxSubmit({
                success: function () {
                    toastr.success("Saved to database", "Update Todo");
                    $('#new-todo').val('');
                    $("#todoapp").triggerHandler("db_action_complete");
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    toastr.error(errorThrown, "Update Action Captured");
                },
                target: '#todo-list'
            });
        },
        MarkComplete: function (form) {
            $(form).ajaxSubmit({
                success: function () {
                    $('#new-todo').focus();
                    toastr.success("Saved to database");
                    $("#todoapp").triggerHandler("db_action_complete");
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    toastr.error(errorThrown, "Completed Action Captured");
                },
                target: '#todo-list'
            });
        },
        Delete: function (form) {
            if (!confirm("Are you sure you want to delete this?")) {
                return false;
            }

            $(form).ajaxSubmit({
                success: function () {
                    $('#new-todo').focus();
                    toastr.success("Deleted from database");
                    $("#todoapp").triggerHandler("db_action_complete");
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
    $.getScript('/Scripts/hasher.min.js', function () {
        var todo_list = $('#todo-list');
        var filters = $('#filters');

        Hasher.add("/active", function () {
            todo_list.find('li')
                .show();

            todo_list.find('li.completed')
                .hide();

            filters.find('li a')
                .removeClass('selected')
                .filter('[href$="active"]')
                .addClass('selected');
        });

        Hasher.add("/completed", function () {
            todo_list.find('li')
                .hide();

            todo_list.find('li.completed')
                .show();

            filters.find('li a')
                .removeClass('selected')
                .filter('[href$="completed"]')
                .addClass('selected');
        });

        Hasher.add("/", function () {
            todo_list.find('li')
                .show();

            filters.find('li a')
                .removeClass('selected')
                .filter('[href="#/"]')
                .addClass('selected');
        });

        // Setup the Hasher
        Hasher.setup();
    });

    $("#todoapp").on("submit", function (event) {
        switch (true) {
            case /Todo\/Create/.test(event.target.action):
                // Matched 'Create' test
                event.preventDefault();
                Todo_Actions.Create(event.target);
                break;
            case /Todo\/MarkComplete/.test(event.target.action):
                // Matched 'MarkComplete' test
                event.preventDefault();
                Todo_Actions.MarkComplete(event.target);
                break;
            case /Todo\/Update/.test(event.target.action):
                // Matched 'Update' test
                event.preventDefault();
                Todo_Actions.Update(event.target);
                break;
            case /Todo\/Delete/.test(event.target.action):
                // Matched 'Delete' test
                event.preventDefault();
                Todo_Actions.Delete(event.target);
                break;
            default:
                console.log("• Didn't match any test");
                break;
        }
    });

    $("#todoapp").on("db_action_complete", function (event) {
        var completed_count = $('#todo-list').find('li.completed').length;
        var active_count = $('#todo-list').find('li').not('.completed').length;

        $('#todo-count strong').text(active_count);
        $('#clear-completed').text('Clear completed (' + completed_count + ')');
    });

    $('#todo-list').on('dblclick', 'li', function (e) {
        var self = $(this);
        self.addClass('editing');

        var txtbox = self.find(':text');
        var original_value = txtbox.val();

        txtbox.data("original_value", original_value);
        txtbox.focus();
    });

    $('#todo-list').on('keypress', '.editing input', function (e) {
        var self = $(this);
        if (e.which === 13) {
            e.preventDefault();
            self.blur();
        }
    });

    $('#todo-list').on('blur', '.editing input', function (e) {
        var self = $(this);

        var orginial_value = self.data("original_value");
        var new_value = self.val();

        if (orginial_value !== new_value) {
            self.closest('form').trigger('submit');
        } else {
            toastr.info("No change");
            var parent_li = self.closest('li');
            parent_li.removeClass('editing');
        }
    });

    $('#toggle-all').on('change', function (e) {
        var checked = $(e.target).prop('checked');
        var todos = $('#todo-list li');

        if (checked)
        {
            todos = todos.not('.completed');
        }
            

        todos
            .find(':checkbox')
            .prop('checked', checked)
            .trigger('change');
    });

    $("#todoapp").triggerHandler("db_action_complete");
})(window, jQuery, toastr);