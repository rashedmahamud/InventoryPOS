
var screen_width = screen.width;
$(window).load(function(){
    if(screen_width < 460) {
        setTimeout(() => {
            $('.loader').fadeOut();
        }, 1000);
    } else {
        $('.loader').fadeOut();
    }

});

// rain fall height
var doc_height = $(document).height();
$(".winter-is-coming").height(doc_height);

function showMenu(x) {
    // x.classList.toggle("change");
    $(".menu-wrapper").addClass('showmenu');
}
function showReservation(x) {
    // x.classList.toggle("change");
    $(".reservation-wrapper").addClass('showmenu');
}

function hideMenu(x) {
    $(".menu-wrapper").removeClass('showmenu');
}

function hideReservation(x) {
    $(".reservation-wrapper").removeClass('showmenu');
}

function openReserTime(x) {
    var idd = $(x);
    var list = idd.siblings('ul').slideToggle();
    console.log(list);
}

function selectData(ele) {
    var value = $(ele).text();
    $(ele).parent().siblings('select').val(value);
    $(ele).parent().siblings('span').children('.label').text(value);
    $(ele).parent().hide();
    console.log(value);
}
 
// handle links with @href started with '#' only
$(document).on('click', 'a[href^="#scroll"]', function(e) {
    // target element id
    var id = $(this).attr('href');

    // target element
    var $id = $(id);
    if ($id.length === 0) {
        return;
    }

    // prevent standard hash navigation (avoid blinking in IE)
    e.preventDefault();

    // top position relative to the document
    var pos = $id.offset().top - 50;

    // animated top scrolling
    $('body, html').animate({scrollTop: pos});
});

var reservation_policy_i = 0;
$(document).ready(function() {
    $(window).scroll(function() {
        if ($(document).scrollTop() > 50) {
            $(".page-title").addClass("pos-init");
        } else {
            $(".page-title").removeClass("pos-init");
        }
    });
    
    $("#reservation_policy").click(function() {
        if(reservation_policy_i % 2 == 0) {
            $(this).siblings('p').show();
            $(".reservation-wrapper").animate({scrollTop: 500}, 200);
            $(".bottom-content").animate({top: 500}, 100);
            reservation_policy_i++;
        } else {
            $(this).siblings('p').hide(50);
            $(".reservation-wrapper").animate({scrollTop: 0}, 500);
            $(".bottom-content").animate({top: 'initial',bottom: 10}, 500);
            reservation_policy_i++;
        }
    });

    
});


// Calendar
var today = new Date();
var dd = today.getDate();
var mm = today.getMonth()+1; 
var yyyy = today.getFullYear();
today = yyyy+'-'+mm+'-'+dd;

jQuery('.datepicker').datetimepicker({
    timepicker:false,
    format:'Y-m-d',
    minDate:today
});
// Calendar End


