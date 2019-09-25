$(document).ready(function () {
    
    var WebApiServiceUrl = 'http://localhost:57236/api/MoviesAndSeries/1';
    var datam = { "Episode_Id": "4", "Title": "A New Hope", "Opening_Crawl": "It is a period of civil war.\r\nRebel spaceships, striking\r\nfrom a hidden base, have won\r\ntheir first victory against\r\nthe evil Galactic Empire.\r\n\r\nDuring the battle, Rebel\r\nspies managed to steal secret\r\nplans to the Empire's\r\nultimate weapon, the DEATH\r\nSTAR, an armored space\r\nstation with enough power\r\nto destroy an entire planet.\r\n\r\nPursued by the Empire's\r\nsinister agents, Princess\r\nLeia races home aboard her\r\nstarship, custodian of the\r\nstolen plans that can save her\r\npeople and restore\r\nfreedom to the galaxy....", "Director": "George Lucas", "Producer": "Gary Kurtz, Rick McCallum", "Created": "2014-12-10T14:23:31.880000Z", "Url": "https://swapi.co/api/films/1/" };
    
    $.ajax({
        url: WebApiServiceUrl,      
        type: "GET",
        contentType: "application/json; charset; charset=utf-8",
            dataType: 'json', // type of response data
        timeout: 90000,     // timeout a minute
        data: { username: "" , password: "" },
        async: false,
        cache : false,
        success: function (data) {   // success callback function
           
      
            //alert(JSON.stringify(datam));

            var counter = 1;
            //Clear previous data
            $('#Movie-Info-From-Service').empty();
            $.each(data, function (key, movieInfo) {

                //build table contents
                $('#Movie-Info-From-Service').append(
                    '<tr>'
                    + '<th>' + counter + '</th>'
                    + '<td>' + data.Title + '</td>'
                    + '<td>' + data.Director + '</td>'
                    + '<td>' + data.Producer + '</td>'
                    + '<td>' + data.Opening_Crawl + '</td>'
                    + '<td>' + data.Created + '</td>'
                    + '</tr >'
                );

                counter++;

            }); 

            /*
            var i = data.Results.length; while (i--) {
                alert('<p>' + data.Results[i].Title + '</>');
            } */
            //    alert(JSON.stringify(data));
            /*
                        var json = $.parseJSON(data);
                        $(json).each(function (i, val) {
                            $.each(val, function (k, v) {
                                alert(k + " : " + v);
                            });
                        }); */

            //loop through data
            /* $.each(data.Results, function (key, movieInfo) {
                 $('#Movie-Info-From-Service').empty();
                 
                 //Clear previous data
                 $('#Movie-Info-From-Service').empty();
                 //build table contents
                 $('#Movie-Info-From-Service').append(
                     '<tr>'
                     + '< th scope = "row" >' + key + '</th >'
                     + '<td>' + movieInfo.Title + '</td>'
                     + '<td>' + movieInfo.Director + '</td>'
                     + '<td>' + movieInfo.Producer + '</td>'
                     + '<td>' + movieInfo.Opening_Crawl + '</td>'
                     + '<td>' + movieInfo.Created + '</td>'
                     + '</tr >'
                 ); 
                 
             });  */

            },

        error: function (xhr, status, error) { // error callback 

            $('#Search-Result-Status').empty();
            $('#Search-Result-Status').append('Error: ' + error);
       
        }
        }); 
    
    //var requestData = {};

    //$.get(WebApiServiceUrl,
    //    {test: ""},
    //    function (data) {
    //        alert("Data: " + data + "\nStatus: " + status);
    //    });
        
});