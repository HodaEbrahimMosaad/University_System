﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
    
});

function showInPopup(url, title) {
    //alert("eeeeeeeeeeeeee");
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            //$("#form-modal .modal-body form").reset();
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');

        }
    })
    
}


    jQueryAjaxPost = form => {
    
        try {
            
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        $('#ViewAll').html(res.html)
                        $('#form-modal .modal-body').html('');
                        $('#form-modal .modal-title').html('');
                        //document.getElementById("courseForm").reset();
                        $('#form-modal').modal('hide');

                        alert(";;")
                        $.notify('Submitted Successfuly', { globalPosition: 'top center', className: 'success' })
                        
                    }
                    else
                        $('#form-modal .modal-body').html(res.html);

                },
                error: function (err) {
                    console.log(err)
                }
            })
            //to prevent default form submit event
            
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }

jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    alert("h")
                    $('#ViewAll').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }
    //prevent default form submit event
    return false;
}