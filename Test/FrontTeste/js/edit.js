$(document).ready(function(){
    if(sessionStorage.getItem("token") == "" || sessionStorage.getItem("token") == null){
        window.location = "login.html";
    }else{
        if(sessionStorage.getItem("editId") == "" || sessionStorage.getItem("editId") == null){
            window.location = "login.html";
        }else{
            GetAddressById();
        }
        //GetAddresslist();
    }
});

$('#messageModal').on('hidden.bs.modal', function (e) {
    $("#titleMessageModal").html("");
    $("#responseSuccess").css("display","none");
    $("#responseSuccess").html("");
    $("#responseError").css("display","none");
    $("#responseError").html("");
});

function LogOut(){
    if(sessionStorage.getItem("token") != "" && sessionStorage.getItem("token") != null){
        sessionStorage.clear("token");
        window.location = "index.html";
    }
}

function editAddress() {

    if($("#description").val() == "" || $("#description").val() == undefined || $("#description").val() == null){
        $('#messageModal').modal('show');
        $("#titleMessageModal").html("Error");
        $("#responseError").css("display","block");
        $("#responseError").html("Address is required");
        return;
    }

    if($("#number").val() == "" || $("#number").val() == undefined || $("#number").val() == null){
        $('#messageModal').modal('show');
        $("#titleMessageModal").html("Error");
        $("#responseError").css("display","block");
        $("#responseError").html("Number is required");
        return;
    }

    if($("#neighborhood").val() == "" || $("#neighborhood").val() == undefined || $("#neighborhood").val() == null){
        $('#messageModal').modal('show');
        $("#titleMessageModal").html("Error");
        $("#responseError").css("display","block");
        $("#responseError").html("Neighborhood is required");
        return;
    }

    var options = {};
    options.url = "https://localhost:44356/api/Address";
    options.type = "PUT";
    var obj = {};
    obj.id = parseInt($("#id").val());
    obj.description = $("#description").val();
    obj.number = $("#number").val();
    obj.complement = $("#complement").val();
    obj.neighborhood = $("#neighborhood").val();
    options.data = JSON.stringify(obj);
    options.contentType = "application/json";
    options.dataType = "json";
    options.beforeSend = function (request) {
        request.setRequestHeader("Authorization", "Bearer " + sessionStorage.getItem("token"));
    };
    options.success = function (obj) {
            
        sessionStorage.removeItem("editId");
        $('#messageModal').modal('show');
        $("#titleMessageModal").html("Success");
        $("#responseSuccess").css("display","block");
        $("#responseSuccess").html("save success");
        setTimeout(function()
        { 
            window.location = "index.html";
        }, 2000);           
    };
    options.error = function (request, status, error) {

        if(request.responseText == "" || request.responseText == undefined){
            if(request.status == 403){
                request.responseText = "Status: " + request.status + " Error: Without authorization";
            }else{
                request.responseText = "Status: " + request.status + " Error: Request failed with server";
            }
        }
            

        $('#messageModal').modal('show');
        $("#titleMessageModal").html("Error");
        $("#responseError").css("display","block");
        $("#responseError").html(request.responseText);
        if(request.status == 401 || request.status == 0){
            sessionStorage.clear("token");
            setTimeout(function()
            { 
                window.location = "login.html";
            }, 2000);
            return;
        }
    };
    $.ajax(options);
}

function GetAddressById(){

    var options = {};
    options.url = "https://localhost:44356/api/Address/" + encodeURIComponent(sessionStorage.getItem("editId"));
    options.type = "GET";
    options.beforeSend = function (request) {
        request.setRequestHeader("Authorization", "Bearer " + sessionStorage.getItem("token"));
    };
    options.dataType = "json";
    options.success = function (data) {

        if(data != null && data != undefined && data != ""){
            $('#description').val(data.description);
            $('#number').val(data.number);
            $('#complement').val(data.complement);
            $('#neighborhood').val(data.neighborhood);
            $('#id').val(data.id);
        }else{
            $('#messageModal').modal('show');
            $("#titleMessageModal").html("Error");
            $("#responseError").css("display","block");
            $("#responseError").html("Address not found");
        }
    };
    options.error = function (request, message, message) {  

        if(request.responseText == "" || request.responseText == undefined)
            request.responseText = "Status: " + request.status + " Error: Request failed with server";//GetErrors(request);
        
        $('#messageModal').modal('show');
        $("#titleMessageModal").html("Error");
        $("#responseError").css("display","block");
        $("#responseError").html(request.responseText);
        if(request.status == 401 || request.status == 0){
            sessionStorage.clear("token");
            setTimeout(function()
            { 
                window.location = "login.html";
            }, 2000);
            return;
        }
    };
    $.ajax(options);

}