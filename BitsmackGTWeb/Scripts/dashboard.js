$(function() {
    buildDashboardList();

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