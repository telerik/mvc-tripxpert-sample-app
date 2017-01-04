/*global window */

(function (TX, $)
{
    var url = "";
    TX.showDialog = function(sender) {
        url = sender.src;
        var dialog = $("#dialog").data("kendoDialog");
        var dataSource = $(dialog.element.context.firstChild).data("kendoListView").dataSource;
        dataSource.fetch();
        dialog.open();
    }

    TX.listViewDataBound = function()
    {
        var dialog = $("#dialog").data("kendoDialog");
        if (this.dataSource.data().length != 0) {
            dialog.center();
            dialog.title(this.dataSource.data()[0].DestinationTitle);
        }
    }

    TX.imageInfo = function()
    {
        return { imageURL: url };
    }
})(window.TX = window.TX || {}, $);