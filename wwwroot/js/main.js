// ==================================================
// Project Name  :  Madrasha
// File          :  JS Base
// Version       :  1.0.0  
// Author        :  Bitspeck
// Developer:    :  MD.ABDULLAH FAHAD GAZI
// ==================================================


$(document).ready(function() {

// preloader - start

  $(window).on('load', function () {
    $('#preloader').fadeOut('slow', function () { $(this).remove(); });
  });
  setTimeout(function()
  { $('#preloader').addClass('d-none'); }, 3000);
  
// preloader - end

var headerId = $(".sticky-header");
  var headerTop = $(".sticky-header .header_top_area");

  $(window).on('scroll', function () {
    var amountScrolled = $(window).scrollTop();
    if ($(this).scrollTop() > 50) {
      headerId.removeClass("not-stuck");
      headerId.addClass("stuck");
      headerTop.addClass("display-none");
    } else {
      headerId.removeClass("stuck");
      headerId.addClass("not-stuck");
      headerTop.removeClass("display-none");
    }
  });

  // preloader - end



    // clientsay - end

    // .client - start

    $('.client').owlCarousel({
    loop:false,
    margin:10,
    nav:true,
    dots:false,
    autoplay:true,
    autoplayTimeout:5000,
    responsive:{
        0:{
            items:1
        },
        500:{
            items:1
        },
        600:{
            items:2
        },
        1000:{
            items:5
        }
    }
})
     // .client - end

    $('.clientsay2').owlCarousel({
    loop:false,
    margin:10,
    nav:false,
    dots : true,
    autoplay:true,
    autoplayTimeout:3000,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:1
        },
        1000:{
            items:1
        }
    }
})




$('.banner').owlCarousel({
    loop:true,
    margin:10,
    nav:false,
    dots:false,
    autoplay:true,
    autoplayTimeout:5000,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:1
        },
        1000:{
            items:1
        }
    }
})


// count js - start

$(document).ready(function() {

$('.counter').each(function () {
$(this).prop('Counter',0).animate({
Counter: $(this).text()
}, {
duration: 4000,
easing: 'swing',
step: function (now) {
$(this).text(Math.ceil(now));
}
});
});

});

//-----------------------------------------------
 // top-to-back - start 
 // ----------------------------------------------
if ($(window).scrollTop() < 100) {
    $('.scrollToTop').hide();
  }
  
  $(window).scroll(function() {
    if ($(this).scrollTop() > 100) {
      $('.scrollToTop').fadeIn('slow');
    } else {
      $('.scrollToTop').fadeOut('slow');
    }
  });
  $('.scrollToTop').click(function(){
    $('html, body').stop().animate({scrollTop:0}, 500, 'swing');
    return false;
  });
  
// -----------------------------------------------
 // top-to-back - start 
 // ----------------------------------------------

$('#contact_form .from-button').click(function () {
    $.ajax({
        type: 'post',
        url: 'mail.php',
        data: $('#contact_form').serialize(),
        success: function () {
          $('#contact_form')[0].reset();
          $('#contact_form .from-button').attr('style', "background-color:#16C39A");
          $('#contact_form .from-button').siblings().html("<i style='color:#16C39A'>*</i> Email has been sent successfully");
        }
    });
    return false;
});

// This is wow js

  new WOW().init();


});

//Courses section

//___faQ QUESTION___//

$(function(){
    //$(".chevron-down").
    $("div[data-toggle=collapse]").click(function(){
        $(this).children('span').toggleClass("fa-chevron-down fa-chevron-up");
    });
})


//gallery//
$(document).ready(function(){

    $(".filter-button").click(function(){
        var value = $(this).attr('data-filter');
        
        if(value == "all")
        {
            //$('.filter').removeClass('hidden');
            $('.filter').show('1000');
        }
        else
        {
//            $('.filter[filter-item="'+value+'"]').removeClass('hidden');
//            $(".filter").not('.filter[filter-item="'+value+'"]').addClass('hidden');
            $(".filter").not('.'+value).hide('3000');
            $('.filter').filter('.'+value).show('3000');
            
        }
    });
    
    if ($(".filter-button").removeClass("active")) {
$(this).removeClass("active");
}
$(this).addClass("active");

});