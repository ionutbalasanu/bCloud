﻿function fileupload(filename) {
    var inputfile = document.getElementById(filename);
    var files = inputfile.files;
    var fdata = new FormData();
    for (var i = 0; i != files.length; i++) {
        fdata.append("files", files[i]);
    }
    $.ajax(

        {
            url:"/uploadmultiple",
            data: fdata,
            processData: false,
            contentType: false,
            type: "POST",
            succes: function (data) {
                alert("File Upload Successfully");
            }
        }
    );
}