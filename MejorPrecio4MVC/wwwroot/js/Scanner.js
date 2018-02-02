$(function() {
    var cxt = canvas.getContext("2d");
    canvas = document.getElementById("canvas");
    video = document.getElementById("video");

    if(!navigator.getUserMedia)
        navigator.getUserMedia = navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia;
    if(!window.URL)
        window.URL = window.webkitURL;

    if (navigator.getUserMedia) {
        navigator.getUserMedia({
            "video" : true,
            "audio": false
        }, function(stream) {
            video.src = window.URL.createObjectURL(stream);
            video.play();
        },function(err){
            console.log("Ocurrió el siguiente error: " + err);
        });
    }
    else{
        alert("getUserMedia no disponible");
        return;
    }

    // Evento click para capturar una foto.
    $("#foto").click(function() {
        cxt.drawImage(video, 0, 0, 450, 368);
    });

    // Evento click para enviar la foto al servidor.
    $("#enviar").click(function() {
        var data = canvas.toDataURL("image/jpeg");
        $.ajax({
            type : "POST",
            url : "http://localhost:5001/Product/IsItAdded",
            contentType : "canvas/jpeg",
            data : data,
            success : function(result) {
                console.log("result:", result);
            }
        });
    });
});