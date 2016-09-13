
define(function() {
    var isTest = true,
        coreVersion = "0.2.3",
        userAgent = navigator.userAgent.toLowerCase(),
        isOpera = ((userAgent.indexOf("opera") !== -1) || (typeof(window.opera) != "undefined")),
        isSaf = ((userAgent.indexOf("applewebkit") !== -1) || (navigator.vendor === "Apple Computer, Inc.")),
        isWebtv = (userAgent.indexOf("webtv") !== -1),
        isIe = ((userAgent.indexOf("msie") !== -1) && (!isOpera) && (!isSaf) && (!isWebtv)),
        isIe7 = ((isIe) && (userAgent.indexOf("msie 7.") !== -1)),
        isMoz = ((navigator.product === "Gecko") && (!isSaf)),
        isKon = (userAgent.indexOf("konqueror") !== -1),
        isNs = ((userAgent.indexOf("compatible") === -1) && (userAgent.indexOf("mozilla") != -1) && (!isOpera) && (!isWebtv) && (!isSaf)),
        isNs4 = ((isNs) && (parseInt(navigator.appVersion) === 4)),
        isMac = (userAgent.indexOf("mac") !== -1),
        isRegexp = (window.RegExp) ? true : false;

    return (function() {
        var log = function(msg) {
                if (!isTest) return;
                var d = new Date();
                console.log(d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds() + ":" + d.getMilliseconds() + " " + msg);
            },
            jIAnPhpEmulator = function() {
                this.strClassName = "jIAn_PHP_Emulator",
                    this.$_GET = (function(a) {
                        if (a === "") return {};
                        var b = {};
                        for (var i = 0; i < a.length; ++i) {
                            var p = a[i].split("=");
                            if (p.length !== 2) continue;
                            b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
                        }
                        return b;
                    })(window.location.search.substr(1).split("&")),
                    this.STR_PAD_LEFT = 1,
                    this.STR_PAD_RIGHT = 2,
                    this.STR_PAD_BOTH = 3,
                    this.str_pad = function(input, padLength, padString, padType) {
                        if (typeof(input) == "number") {
                            input = input.toString();
                        }
                        if (typeof(padLength) == "undefined") {
                            padLength = 0;
                        }
                        if (typeof(padString) == "undefined") {
                            padString = " ";
                        }
                        if (typeof(padType) == "undefined") {
                            padType = this.STR_PAD_LEFT;
                        }

                        if (padLength + 1 >= input.length) {
                            switch (padType) {
                            case this.STR_PAD_LEFT:
                                input = Array(padLength + 1 - input.length).join(padString) + input;
                                break;
                            case this.STR_PAD_BOTH:
                                var padlen;
                                var right = Math.ceil((padlen = padLength - input.length) / 2);
                                var left = padlen - right;
                                input = Array(left + 1).join(padString) + input + Array(right + 1).join(padString);
                                break;
                            default:
                                input = input + Array(padLength + 1 - input.length).join(padString);
                                break;
                            } // switch

                        }
                        return input;

                    },
                    this.round = function(input, precision) {
                        if (typeof(precision) == "undefined") {
                            precision = 0;
                        }
                        return Math.round(input * Math.pow(10, precision)) / Math.pow(10, precision);
                    },
                    this.getClassName = function() {
                        return this.strClassName;
                    },
                    this.ltrim = function(str) {
                        if (typeof(str) == "undefined") return "";
                        return str.replace(/^\s+/g, "");
                    },
                    this.rtrim = function(str) {
                        if (typeof(str) == "undefined") return "";
                        return str.replace(/(\s+)$/g, "");
                    },
                    this.trim = function(str) {
                        return this.ltrim(this.rtrim(str));
                    },
                    this.urlencode = function(text) {
                        text = escape(text.toString()).replace(/\+/g, "%2B");
                        var matches = text.match(/(%([0-9A-F]{2}))/gi);
                        if (matches) {
                            for (var matchid = 0; matchid < matches.length; matchid++) {
                                var code = matches[matchid].substring(1, 3);
                                if (parseInt(code, 16) >= 128) {
                                    text = text.replace(matches[matchid], "%u00" + code);
                                }
                            }
                        }
                        text = text.replace("%25", "%u0025");
                        return text;
                    },
                    this.in_array = function(ineedle, haystack, caseinsensitive) {
                        var needle = new String(ineedle);
                        var i;
                        if (caseinsensitive) {
                            needle = needle.toLowerCase();
                            for (i in haystack) {
                                if (haystack[i].toLowerCase() == needle) {
                                    return i;
                                }
                            }
                        } else {
                            for (i in haystack) {
                                if (haystack[i] == needle) {
                                    return i;
                                }
                            }
                        }
                        return -1;
                    },
                    this.htmlspecialchars = function(str) {
                        //var f = new Array(/&(?!#[0-9]+;)/g, /</g, />/g, /"/g);
                        var f = new Array(
                            (isMac && isIe ? new RegExp("&", "g") : new RegExp("&(?!#[0-9]+;)", "g")),
                            new RegExp("<", "g"),
                            new RegExp(">", "g"),
                            new RegExp("\"", "g")
                        );
                        var r = new Array(
                            "&amp;",
                            "&lt;",
                            "&gt;",
                            "&quot;"
                        );

                        for (var i = 0; i < f.length; i++) {
                            str = str.replace(f[i], r[i]);
                        }
                        return str;
                    },
                    this.unhtmlspecialchars = function(str) {
                        var f = new Array(/&lt;/g, /&gt;/g, /&quot;/g, /&amp;/g, /&nbsp;/g);
                        var r = new Array("<", ">", "\"", "&", " ");

                        for (var i in f) {
                            str = str.replace(f[i], r[i]);
                        }

                        return str;
                    },
                    this.unescape_cdata = function(str) {
                        var r1 = /<\=\!\=\[\=C\=D\=A\=T\=A\=\[/g;
                        var r2 = /\]\=\]\=>/g;

                        return str.replace(r1, "<![CDATA[").replace(r2, "]]>");
                    },
                    this.formatNumber = function(number, pattern) {

                        var str = number.toString();
                        var strInt;
                        var strFloat;
                        var formatInt;
                        var formatFloat;
                        if (/\./g.test(pattern)) {
                            formatInt = pattern.split(".")[0];
                            formatFloat = pattern.split(".")[1];
                        } else {
                            formatInt = pattern;
                            formatFloat = null;
                        }

                        if (/\./g.test(str)) {
                            if (formatFloat != null) {
                                var tempFloat = Math.round(parseFloat("0." + str.split(".")[1]) * Math.pow(10, formatFloat.length)) / Math.pow(10, formatFloat.length);
                                strInt = (Math.floor(number) + Math.floor(tempFloat)).toString();
                                strFloat = /\./g.test(tempFloat.toString()) ? tempFloat.toString().split(".")[1] : "0";
                            } else {
                                strInt = Math.round(number).toString();
                                strFloat = "0";
                            }
                        } else {
                            strInt = str;
                            strFloat = "0";
                        }
                        var zero;
                        if (formatInt != null) {
                            var outputInt = "";
                            zero = formatInt.match(/0*$/)[0].length;
                            var comma = null;
                            if (/,/g.test(formatInt)) {
                                comma = formatInt.match(/,[^,]*/)[0].length - 1;
                            }
                            var newReg = new RegExp("(\\d{" + comma + "})", "g");

                            if (strInt.length < zero) {
                                outputInt = new Array(zero + 1).join("0") + strInt;
                                outputInt = outputInt.substr(outputInt.length - zero, zero);
                            } else {
                                outputInt = strInt;
                            }
                            outputInt = outputInt.substr(0, outputInt.length % comma) + outputInt.substring(outputInt.length % comma).replace(newReg, (comma != null ? "," : "") + "$1");
                            outputInt = outputInt.replace(/^,/, "");

                            strInt = outputInt;
                        }

                        if (formatFloat != null) {
                            var outputFloat = "";
                            zero = formatFloat.match(/^0*/)[0].length;
                            if (strFloat.length < zero) {
                                outputFloat = strFloat + new Array(zero + 1).join("0");
                                //outputFloat        = outputFloat.substring(0,formatFloat.length);
                                var outputFloat1 = outputFloat.substring(0, zero);
                                var outputFloat2 = outputFloat.substring(zero, formatFloat.length);
                                outputFloat = outputFloat1 + outputFloat2.replace(/0*$/, "");
                            } else {
                                outputFloat = strFloat.substring(0, formatFloat.length);
                            }

                            strFloat = outputFloat;
                        } else {
                            if (pattern != "" || (pattern == "" && strFloat == "0")) {
                                strFloat = "";
                            }
                        }

                        return strInt + (strFloat == "" ? "" : "." + strFloat);

                    }, this.getParameterByName = function(name) {
                        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
                        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                            results = regex.exec(location.search);
                        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
                    };
            },
            php = new jIAnPhpEmulator(),
            jIAnUtils = function() {
                this.UtcToTimezone = function(utcDateString) {
                    var convertdLocalTime = new Date(utcDateString);
                    var hourOffset = convertdLocalTime.getTimezoneOffset() / 60;
                    convertdLocalTime.setHours(convertdLocalTime.getHours() + hourOffset);
                    return convertdLocalTime;
                }
            },
            jIAnHiddenForm = function() {
                this.aryVariables = new Array(),
                    this.strClassName = "jIAn_Hidden_Form",
                    this.getClassName = function() {
                        return this.strClassName;
                    },
                    this.add_variable = function(argStrKey, argStrValue) {
                        var isNewItem = true;
                        for (var i = 0; i < this.aryVariables.length; i++) {
                            if (this.aryVariables[i][0] === argStrKey) {
                                this.aryVariables[i][1] = argStrValue;
                                isNewItem = false;
                                break;
                            }
                        }
                        if (isNewItem) {
                            this.aryVariables[this.aryVariables.length] = new Array(argStrKey, argStrValue);
                        }
                    },
                    this.add_variables_from_object = function(argObjForm) {
                        var objInputs = fetch_tags(argObjForm, "input");
                        var i;
                        for (i = 0; i < objInputs.length; i++) {
                            switch (objInputs[i].type) {
                            case "checkbox":
                            case "radio":
                                if (objInputs[i].checked) {
                                    this.add_variable(objInputs[i].name, objInputs[i].value);
                                }
                                break;
                            case "text":
                            case "hidden":
                            case "password":
                                this.add_variable(objInputs[i].name, objInputs[i].value);
                                break;
                            default:
                                continue;
                            }
                        }

                        var objTextareas = fetch_tags(argObjForm, "textarea");
                        for (i = 0; i < objTextareas.length; i++) {
                            this.add_variable(objTextareas[i].name, objTextareas[i].value);
                        }

                        var objSelects = fetch_tags(argObjForm, "select");
                        for (i = 0; i < objSelects.length; i++) {
                            if (objSelects[i].multiple) {
                                for (var j = 0; j < objSelects[i].options.length; j++) {
                                    if (objSelects[i].options[j].selected) {
                                        this.add_variable(objSelects[i].name, objSelects[i].options[j].value);
                                    }
                                }
                            } else {
                                this.add_variable(objSelects[i].name, objSelects[i].options[objSelects[i].selectedIndex].value);
                            }
                        }
                    },
                    this.fetch_variable = function(argStrVariable) {
                        for (var i = 0; i < this.aryVariables.length; i++) {
                            if (this.aryVariables[i][0] === argStrVariable) {
                                return this.aryVariables[i][1];
                            }
                        }
                        return null;
                    },
                    this.clear_variable = function() {
                        this.aryVariables = new Array();
                        return null;
                    },
                    this.submit_form = function(argStrSubmitMethod, argStrTargetScript, argStrTarget) {
                        if (typeof(argStrTarget) == "undefined") {
                            argStrTarget = "_self";
                        }

                        this.objForm = document.createElement("form");
                        this.objForm.method = argStrSubmitMethod;
                        this.objForm.action = argStrTargetScript;
                        this.objForm.target = argStrTarget;

                        for (var i = 0; i < this.aryVariables.length; i++) {
                            var objInput = document.createElement("input");

                            objInput.type = "hidden";
                            objInput.name = this.aryVariables[i][0];
                            objInput.value = this.aryVariables[i][1];

                            this.objForm.appendChild(objInput);
                        }

                        document.body.appendChild(this.objForm).submit();
                    },
                    this.add_form = function(argFormId, argStrSubmitMethod, argStrTargetScript) {
                        this.objNewForm = document.createElement("form");
                        this.objNewForm.id = argFormId;
                        this.objNewForm.name = argFormId;
                        this.objNewForm.method = argStrSubmitMethod;
                        this.objNewForm.action = argStrTargetScript;

                        for (var i = 0; i < this.aryVariables.length; i++) {
                            var objInput = document.createElement("input");
                            objInput.type = "hidden";
                            objInput.name = this.aryVariables[i][0];
                            objInput.value = this.aryVariables[i][1];
                            this.objNewForm.appendChild(objInput);
                        }
                        document.body.appendChild(this.objNewForm);
                    },
                    this.build_query_string = function() {
                        //var queryString = "";
                        var tmpArray = new Array();
                        for (var i = 0; i < this.aryVariables.length; i++) {
                            console.log(this.aryVariables[i][0]);
                            console.log(this.aryVariables[i][1]);
                            tmpArray[i] = this.aryVariables[i][0] + "=" + php.urlencode(this.aryVariables[i][1]);
                            //queryString += this.aryVariables[i][0] + "=" + php.urlencode(this.aryVariables[i][1]) + "&";
                        }
                        return tmpArray.join("&");
                        //return queryString;
                    };
            };
        jIAnHiddenForm.prototype =
        {
            fetch_tags: function(parentobj, tag) {
                if (typeof parentobj.getElementsByTagName != "undefined") {
                    return parentobj.getElementsByTagName(tag);
                } else if (parentobj.all && parentobj.all.tags) {
                    return parentobj.all.tags(tag);
                } else {
                    return null;
                }
            }
        };
        var hiddenForm = new jIAnHiddenForm();
        var utils = new jIAnUtils();
        log("jIAn ver is " + coreVersion);
        log("userAgent is " + userAgent);
        return { log: log, form: hiddenForm, PHP: php, Utils: utils };
    })();
});