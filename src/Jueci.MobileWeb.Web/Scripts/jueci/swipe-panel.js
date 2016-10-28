$(function () {
    $(".tab-container").on("swipeleft", function () {
        var currentTabIndex = $(".ui-state-default[aria-selected=true]").data('tabindex');
        var nextTabIndex = currentTabIndex + 1;

        if (currentTabIndex === $('.ui-state-default').length) {
            return;
        }
        if (mobilecheck()) {
            switchPlanDetailTab(currentTabIndex, nextTabIndex);
        }

    });

    $(".tab-container").on("swiperight", function () {
        var currentTabIndex = $(".ui-state-default[aria-selected=true]").data('tabindex');

        var nextTabIndex = currentTabIndex - 1;
        if (currentTabIndex === 1) {
            return false;
        }
        if (mobilecheck()) {
            switchPlanDetailTab(currentTabIndex, nextTabIndex);
        }       
    });

    $(".ui-state-default").on('click', function () {

        //var currentTabIndex = $(".ui-state-default[aria-selected=true]").data('tabindex');

        var beferTabIndex = $(".ui-tabs-active[tabindex=-1]").data('tabindex');

        var currentTabIndex = $(this).data('tabindex');

        switchPlanDetailTab(beferTabIndex, currentTabIndex);
    });
});

function switchPlanDetailTab(beferTabIndex, currentTabIndex) {

    $('.ui-state-default[data-tabindex=' + beferTabIndex + ']').removeClass("ui-state-hover ui-state-focus ui-tabs-active ui-state-active  find_nav_cur");
    $('.ui-state-default[data-tabindex=' + beferTabIndex + ']').attr('aria-selected', "false");
    $('.ui-state-default[data-tabindex=' + beferTabIndex + '] > a').removeClass("ui-btn-active");
    $('.ui-state-default[data-tabindex=' + beferTabIndex + '] > span').removeClass("stress-dot").addClass('clock-hide');
    $('#tab' + beferTabIndex).attr('style', 'background: rgb(255, 255, 255); padding: 0px; display: none;');
    $('#tab' + beferTabIndex).attr('aria-expanded', 'false');
    $('#tab' + beferTabIndex).attr('aria-hidden', 'true');
    //aria-expanded="false" aria-hidden="true"

    $('.ui-state-default[data-tabindex=' + currentTabIndex + ']').addClass("ui-state-hover ui-state-focus ui-tabs-active ui-state-active find_nav_cur");
    $('.ui-state-default[data-tabindex=' + currentTabIndex + ']').attr('aria-selected', "true");
    $('.ui-state-default[data-tabindex=' + currentTabIndex + '] > a').addClass("ui-btn-active");
    $('.ui-state-default[data-tabindex=' + currentTabIndex + '] > span').removeClass("clock-hide").addClass('stress-dot');
    $('#tab' + currentTabIndex).attr('style', 'background: #ffffff; padding: 0 0;');
    $('#tab' + currentTabIndex).attr('aria-expanded', 'true');
    $('#tab' + currentTabIndex).attr('aria-hidden', 'false');

    $('#CurrentTabIndex').val(currentTabIndex);
}