////const swalWithBootstrapButtons = Swal.mixin({
////    customClass: {
////        confirmButton: 'btn btn-success',
////        cancelButton: 'btn btn-danger'
////    },
////    buttonsStyling: false
////})
const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
})
const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000
});
(function ($) {
    "use strict";

    // Initiate the wowjs
    new WOW().init();
    


    // Spinner
    var spinner = function () {
        setTimeout(function () {
            if ($('#spinner').length > 0) {
                $('#spinner').removeClass('show');
            }
        }, 1);
    };
    spinner();

    // Sticky Navbar
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.sticky-top').addClass('shadow-sm').css('top', '0px');
        } else {
            $('.sticky-top').removeClass('shadow-sm').css('top', '-100px');
        }
    });
    
    
    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({scrollTop: 0}, 1500, 'easeInOutExpo');
        return false;
    });


    // Header carousel
    $(".header-carousel").owlCarousel({
        autoplay: true,
        rtl: true,
        smartSpeed: 1500,
        items: 1,
        dots: true,
        loop: true,
        nav : true,
        navText : [
            '<i class="bi bi-chevron-left"></i>',
            '<i class="bi bi-chevron-right"></i>'
        ]
    });


    // Testimonials carousel
    $(".testimonial-carousel").owlCarousel({
        autoplay: true,
        rtl: true,
        smartSpeed: 1000,
        margin: 24,
        dots: false,
        loop: true,
        nav : true,
        navText : [
            '<i class="bi bi-arrow-left"></i>',
            '<i class="bi bi-arrow-right"></i>'
        ],
        responsive: {
            0:{
                items:1
            },
            992:{
                items:2
            }
        }
    });
    
})(jQuery);

GotoSchool = rateNo => {
    localStorage.rateNo = rateNo;
    window.location.href = `/Home/ClientInfo`;
}

GoFirstStep = (clientname, qualification) => {
    localStorage.clientname = clientname;
    localStorage.qualification = qualification;
    window.location.href = "/Home/AboutSchool";
}

GetNextStep = SchoolId => {
    if (SchoolId !== "0") {
        localStorage.SchoolId = SchoolId
        window.location.href = "/Home/QuestionSurvay";
    } else {
        Toast.fire({
            type: 'error',
            title: 'الرجاء اختيار مدرسة!'
        })
    }
}


PostQuestion = (rateNo, schoolNo, qNameAr, answerList, qId) => {
    $.post('/Questions/PostQuestion', { rateNo, schoolNo, qNameAr, answerList, qId:qId }, function (data) {


        if (data.isValid) {
            $("#ddlRate").prop('selectedIndex', 0);
            $("#ddlTerritory").prop('selectedIndex', 0);
            $("#ddlClassification").prop('selectedIndex', 0);
            $("#ddlGender").prop('selectedIndex', 0);
            $("#ddlSchool").prop('selectedIndex', 0);
            $("#ddlLevel").prop('selectedIndex', 0);
            $("#txt-question").val("");
            $("#txt-answer").val("");
            $("#tblanswer > tbody").html("");
            //$('#select-question').hide();
            //$('#select-answer').hide();
            //$('#confirm-question').hide();
            $('#confirm-question').slideDown(1000).hide();
            $('#select-rate').show("fast");
            if (localStorage.qId) {
                Toast.fire({
                    type: 'success',
                    title: 'تم نعديل السؤال بنجاح'
                })
            } else {
                Toast.fire({
                    type: 'success',
                    title: 'تم اضافة السؤال بنجاح'
                })
            }
            
        }
        else
            Toast.fire({
                type: 'error',
                title: 'فشلت عملية الادخال'
            })


    });
}

