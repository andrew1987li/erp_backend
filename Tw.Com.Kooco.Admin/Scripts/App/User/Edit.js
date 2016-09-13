define([
    'jquery',
    'jIAn',
    'jq_uniform',
], function ($, jIAn) {
    var run = function (param) {
        $('.js_all_on').click(function (e) {
            e.preventDefault();

            $('input[name="RoleIDs"]').each(function (index) {
                $(this).bootstrapSwitch('state', true);
            });
        });

        $('.js_all_off').click(function (e) {
            e.preventDefault();

            $('input[name="RoleIDs"]').each(function (index) {
                $(this).bootstrapSwitch('state', false);
            });
        });

        $.ajax({
            type: 'post',
            url: '/User/AjaxDetail',
            data: { 'User.IdentityKey': data.IdentityKey },
            success: function (result) {
                if (result.Ok) {
                    $('#account').val(result.Data.Account);
                    $('#employeeNo').val(result.Data.EmployeeNo);
                    $('#firstName').val(result.Data.FirstName);
                    $('#lastName').val(result.Data.LastName);
                    $('#profession').val(result.Data.ProfessionId);
                    $('#department').val(result.Data.DepartmentId);
                    $('#hospitalDistrict').val(result.Data.HospitalDistrictId);
                    $('#primaryRule_' + result.Data.PrimaryRule).prop("checked", "checked");

                    if (result.Data.RuleGroups.indexOf(',') >= 0) {
                        var ruleGroups = result.Data.RuleGroups.split(',');
                        $.each(ruleGroups, function (index, value) {
                            $("input:checkbox[name=ruleGroups][value=" + value + "]").prop("checked", "checked");
                        });
                    }

                    $('#ruleGroups_' + result.Data.PrimaryRule).prop("checked", "").attr('disabled', 'disabled').css({ 'cursor': 'not-allowed' });
                    $('#ruleGroupsLab_' + result.Data.PrimaryRule).css({ "color": "#d3d3d3", 'cursor': 'not-allowed' }).attr('disabled', 'disabled');
                    $("#status" + result.Data.Status).prop("checked", "checked");
                    $.uniform.update("#status" + result.Data.Status);
                    $('input:radio[name=primaryRule]').change(function () {
                        var key = parseInt($(this).val(), 10);
                        $('input:checkbox[name=ruleGroups]').each(function (index, value) {
                            var val = parseInt($(value).val(), 10);
                            if (key == val) {
                                $('#ruleGroups_' + val).prop("checked", "").attr('disabled', 'disabled').css({ 'cursor': 'not-allowed' });
                                $('#ruleGroupsLab_' + val).css({ "color": "#d3d3d3", 'cursor': 'not-allowed' }).attr('disabled', 'disabled');
                            } else {
                                $('#ruleGroups_' + val).removeAttr("disabled");
                                $('#ruleGroupsLab_' + val).css({ "color": "#000", 'cursor': 'pointer' }).removeAttr("disabled");
                            }
                        });
                    });

                    //$('#detail').css('visibility', 'visible');
                }
            }
        });
        $('.js_back').click(function (e) {
            e.preventDefault();
            var form = jIAn.form;
            form.submit_form('get', "/User/Index");
        });
        $('.js_submit').click(function (e) {
            e.preventDefault();
            $.ajax({
                type: 'post',
                url: '/User/AjaxUpdate',
                data: {
                    'User.IdentityKey': data.IdentityKey,
                    'User.Status': $("input:radio[name=status]:checked").val(),
                    'User.FirstName': $('#firstName').val(),
                    'User.Password': $('#pw').val(),
                    'User.LastName': $('#lastName').val(),
                    'User.ProfessionId': $("#profession option:selected").val(),
                    'User.DepartmentId': $("#department option:selected").val(),
                    'User.HospitalDistrictId': $("#hospitalDistrict option:selected").val(),
                    'User.PrimaryRule': $("input:radio:checked[name=primaryRule]").val(),
                    'User.RoleIDs': $('input:checkbox:checked[name=RoleIDs]').map(function () { return $(this).val(); }).get().join(",") + ",",
                    'User.RuleGroups': $('input:checkbox:checked[name=ruleGroups]').map(function () { return $(this).val(); }).get().join(",")
                },
                success: function (result) {
                    var code = parseInt(result.Code, 10);
                    if (code > 0) {
                        var form = jIAn.form;
                        form.add_variable('IsTransfer', true);
                        form.add_variable('StayTime', 1);
                        form.add_variable('Target', "/User/Edit");
                        form.add_variable('Method', 'post');
                        form.add_variable('Parameter', 'Todo=edit&User.IdentityKey=' + data.IdentityKey);
                        form.add_variable('Message', 'Success');
                        form.submit_form('post', "/Misc/Message");
                    } else {
                        if (code == 0) {
                            $('#warning').text('沒有資料被更新');
                        } else if (code == -1) {
                            $('#warning').text('資料庫異常,系統錯誤。');
                        } else if (code == -2) {
                            $('#warning').text('員工編號已被使用。');
                        }
                        $('#warning').css('display', 'block');
                    }
                }
            });
        });
        $(window).resize(function () {
            resize();
        });
        resize();
    },
resize = function () {
    $('#primaryRuleText').css('height', $('#primaryRuleContent').css('height'));
    $('#ruleGroupsText').css('height', $('#ruleGroupsContent').css('height'));
};
    return (function () { return { run: run }; })();
});