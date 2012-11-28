// =================================
//     Custom jQuery Plugins...
// =================================

(function ($) {
    // ========================================
    // ==========  Simple Pub/Sub  ============
    // ========================================
    var o = $({});
    $.subscribe = function () {
        o.on.apply(o, arguments);
    };
    $.unsubscribe = function () {
        o.off.apply(o, arguments);
    };
    $.publish = function () {
        o.trigger.apply(o, arguments);
    };

    // ========================================
    // ========== Serialize Object  ===========
    // ========================================
    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };

    // ========================================
    // ========== Double Tap Event  ===========
    // ========================================
    $.fn.doubletap = function (fn) {
        return fn ? this.bind('doubletap', fn) : this.trigger('doubletap');
    };

    $.attrFn.doubletap = true;

    $.event.special.doubletap = {
        setup: function (data, namespaces) {
            $(this).bind('touchend', $.event.special.doubletap.handler);
        },

        teardown: function (namespaces) {
            $(this).unbind('touchend', $.event.special.doubletap.handler);
        },

        handler: function (event) {
            var action;

            clearTimeout(action);

            var now = new Date().getTime();
            //the first time this will make delta a negative number
            var lastTouch = $(this).data('lastTouch') || now + 1;
            var delta = now - lastTouch;
            var delay = delay == null ? 500 : delay;

            if (delta < delay && delta > 0) {
                // After we detct a doubletap, start over
                $(this).data('lastTouch', null);

                // set event type to 'doubletap'
                event.type = 'doubletap';

                // let jQuery handle the triggering of "doubletap" event handlers
                $.event.handle.apply(this, arguments);
            } else {
                $(this).data('lastTouch', now);

                action = setTimeout(function (evt) {
                    // set event type to 'doubletap'
                    event.type = 'tap';

                    // let jQuery handle the triggering of "doubletap" event handlers
                    $.event.handle.apply(this, arguments);

                    clearTimeout(action); // clear the timeout
                }, delay, [event]);
            }
        }
    };

})(jQuery);

window.log = function f() {
    log.history = log.history || [];
    log.history.push(arguments);
    if (this.console) {
        var args = arguments,
                        newarr;
        args.callee = args.callee.caller;
        newarr = [].slice.call(args);
        if (typeof console.log === 'object') log.apply.call(console.log, console, newarr);
        else console.log.apply(console, newarr);
    }
};
(function (a) {
    function b() { }
    for (var c = "assert,count,debug,dir,dirxml,error,exception,group,groupCollapsed,groupEnd,info,log,markTimeline,profile,profileEnd,time,timeEnd,trace,warn".split(","), d; !!(d = c.pop()); ) {
        a[d] = a[d] || b;
    }
})(function () { try { console.log(); return window.console; } catch (a) { return (window.console = {}); } } ());