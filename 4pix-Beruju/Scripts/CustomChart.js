google.load('visualization', '1.0', { 'packages': ['corechart'] });  
  
// Set a callback to run when the Google Visualization API is loaded.  
$(document).ready(function ()  
{  
    $.ajax(  
    {  
        type: 'POST',  
        dataType: 'JSON',  
        url: '/LocalLevelHome/GetChartData',  
        success:  
            function (response)  
            {  
                // Set chart options  
                var options =  
                    {  
                        width: 450,  
                        height: 400,  
                        sliceVisibilityThreshold: 0,  
                        legend: { position: "top", alignment: "end" },  
                        chartArea: { left: 110, top: 50, height: "70%" },  
                        hAxis:  
                            {  
                                slantedText: true,  
                                slantedTextAngle: 18  
                            },  
                        bar: { groupWidth: "50%" },  
                    };  
  
                // Draw.  
                drawGraph(response, options, 'graphId');  
            }  
    });  
});  
  
// Callback that creates and populates a data table,  
// instantiates the pie chart, passes in the data and  
// draws it.  
function drawGraph(dataValues, options, elementId) {  
    // Initialization.  
    var data = new google.visualization.DataTable();  
  
    // Setting.  
    data.addColumn('string', 'आर्थिक वर्ष');  
    data.addColumn('number', 'जम्मा अन्तिम बेरूजु');  
    data.addColumn('number', 'जम्मा समपरिक्षण');  
  
    // Processing.  
    for (var i = 0; i < dataValues.length; i++)  
    {  
        // Setting.  
        data.addRow([dataValues[i].FiscalYearTitle, dataValues[i].ExternalBerujuTotal, dataValues[i].SamparikshadTotal]);  
    }  
  
    // Setting label.  
    var view = new google.visualization.DataView(data);  
    view.setColumns([0, 1,  
        {  
            calc: "stringify",  
            sourceColumn: 1,  
            type: "string",  
            role: "annotation"  
        },  
        2,  
        {  
            calc: "stringify",  
            sourceColumn: 2,  
            type: "string",  
            role: "annotation"  
        }  
    ]);  
  
    // Instantiate and draw our chart, passing in some options.  
    var chart = new google.visualization.BarChart(document.getElementById(elementId));  
  
    // Draw chart.  
    chart.draw(view, options);  
} 