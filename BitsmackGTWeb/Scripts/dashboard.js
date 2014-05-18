﻿$(function() {
    loadListView("http://bitsmackgtapi.apphb.com/dashboard/steps");

});

function buildDashboardList() {
    var listview = $("#dashListView");
    $.get("http://bitsmackgtapi.apphb.com/dashboard", function(returnData) {
        $.each(returnData.Categories, function () {
            var divider = $("<li data-role='list-divider'></li>").html(this.Name);
            listview.append(divider);
            $.each(this.Items, function () {
                var item = $("<li></li>");
                if (this.ItemType == "ProgressBar") {
                    var containerDiv = $("<div></div>").append(this.Text + " " + this.Status);
                    var barDiv = $("<div></div>").attr("id", this.Text);
                    containerDiv.append(barDiv);
                    item.append(containerDiv);
                    listview.append(item);
                    var pbar = jQMProgressBar(this.Text)
                        .setOuterTheme('b')
                        .setInnerTheme('e')
                        .isMini(true)
                        .setMax(this.BarMax)
                        .setStartFrom(this.BarMin)
                        .setInterval(10)
                        .showCounter(true)
                        .build();
                    pbar.setValue(this.BarActual);
                }
                else {
                    item.html(this.Text);
                    listview.append(item);
                }
                
            });
        });
        
        listview.listview("refresh");
    });
}

function loadListView(url) {
    var listview = $("#dashListView");
    $.ajax({
        "url":url, 
        "success": function(returnData) {
            var divider = $('<div data-role="collapsible" data-iconpos="right" data-inset="false"></div>');
            divider.append($("<h2></h2>").append(returnData.Title));
            $.each(returnData.Texts, function() {
                divider.append("<p>" + this + "</p>");
            });
            var subList = $("<ul data-role='listview' data-theme='a'></ul>");
            $.each(returnData.Items, function() {
                var item = $("<li></li>").append(this.Title);
                $.each(this.Texts, function () {
                    item.append("<p>" + this + "</p>");
                });
                subList.append(item);
            });
            divider.append(subList);
            listview.append(divider);
            $('ul[data-role="listview"]').listview({ refresh: true });
            $('div[data-role="collapsible"]').collapsible({ refresh: true });
        },
        "complete": function () {           
            
        }
    });

}