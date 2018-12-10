<!DOCTYPE html>
<html lang="en">
<head>

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Rain Forest</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.2.0/css/all.css" integrity="sha384-hWVjflwFxL6sNzntih27bfxkr27PmbbK/iSvJ+a4+0owXq79v+lsFkW54bOGbiDQ" crossorigin="anonymous">
    <link href="css/jquery.datetimepicker.css" rel="stylesheet" type="text/css">
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/responsive.css">
    
    <style>

        .drop {
            background:-webkit-gradient(linear,0% 0%,0% 100%, from(rgba(176, 195, 197) ), to(rgba(255,255,255,0.6))  );
            background: -moz-linear-gradient(top, rgba(13,52,58,1) 0%, rgba(255,255,255,.6) 100%);
            width:2px;
            height:8px;
            position: absolute;
            top:0px;
            z-index: 999;
            -webkit-animation: fall 2s linear infinite;
            -moz-animation: fall 2s linear infinite;
            
        }
        /* animate the drops*/
        @-webkit-keyframes fall {
            to {margin-top:5px;}
        }
        @-moz-keyframes fall {
            to {margin-top:5px;}
        }
    </style>
</head>
<body>

    <div class="loader">
        <img src="images/a1.gif" alt="">
        <img src="images/a2.gif" alt="">
    </div>

    <!-- Site music -->
    <div id="player">
        <audio autoplay hidden loop>
            <source src="rain.mp3" type="audio/mpeg">
                If you're reading this, audio isn't supported. 
        </audio>
    </div>
    <!-- rain visual effect -->
    <div class="winter-is-coming">
        <div class="snow snow--near"></div>
        <div class="snow snow--near snow--alt"></div>
    </div>
    
    <!-- Header -->
    <header class="header-wrapper">
        <div class="inner">
            <div class="row">
                <div class="col-3 col-lg-2 logo">
                    <a href="index.html">
                        <img src="images/logo.jpg" alt="">
                    </a>
                </div>
                <div class="col-3 col-lg-6 menu-icon">
                    <div id="menu_area" class="menu-area">
                        <div class="container">
                            <div class="row">
                                <nav class="navbar navbar-light navbar-expand-lg mainmenu">
                                    <button class="navbar-toggler" type="button" onclick="showMenu(this)">
                                    <span class="navbar-toggler-icon"></span>
                                    </button>
                                    <div class="collapse navbar-collapse" id="navbarSupportedContent">
                                        <ul class="navbar-nav mr-auto">
                                            <li><a href="index.html">Home <span class="sr-only">(current)</span></a></li>
                                            <li><a href="menu.html">Menu</a></li>
                                            <li class="active"><a href="about.html">About</a></li>
                                            <li class="dropdown">
                                                <a class="dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Dropdown</a>
                                                <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                                                <li><a href="#">Action</a></li>
                                                <li><a href="#">Another action</a></li>
                                                <li><a href="#">Something else here</a></li>
                                                <li class="dropdown">
                                                    <a class="dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Dropdown2</a>
                                                    <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                                                    <li><a href="#">Action</a></li>
                                                    <li><a href="#">Another action</a></li>
                                                    <li><a href="#">Something else here</a></li>
                                                    <li class="dropdown">
                                                        <a class="dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Dropdown3</a>
                                                        <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                                                            <li><a href="#">Action</a></li>
                                                            <li><a href="#">Another action</a></li>
                                                            <li><a href="#">Something else here</a></li>
                                                        </ul>
                                                    </li>
                                                    </ul>
                                                </li>
                                                </ul>
                                            </li>
                                            <li><a href="news.html">News</a></li>
                                            <li><a href="contact.html">Contact</a></li>
                                        </ul>
                                    </div>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-lg-4 pull-right right-button">
                    <a href="#" class="rain-btn-spe" onclick="showReservation(this)">Make <br> Reservation</a>
                    <a href="#" class="rain-btn-spe">Make <br> Online Order</a>
                </div>
            </div>
        </div>
        <!-- <div class="page-title">
            <h1>Home</h1>
        </div> -->
    </header>
    <!-- Header END -->

    <!-- Menu -->
    <div class="menu-wrapper">
        <div class="menu-inner">
            <div class="menu_icon close-btn change" onclick="hideMenu(this)">
                <div class="bar1"></div>
                <div class="bar2"></div>
                <div class="bar3"></div>
            </div>
            <ul>
                <li class="active"><a href="index.html">Home</a></li>
                <li><a href="menu.html">Menu</a></li>
                <li><a href="about.html">About</a></li>
                <li><a href="news.html">News</a></li>
                <li><a href="contact.html">Contact</a></li>
            </ul>
        </div>
        <a href="#" class="rain-btn menu-rain-btn" onclick="showReservation(this)">Make A Reservation</a>
    </div>
    <!-- Menu END -->

    <!-- Reservation -->
    <div class="reservation-wrapper">
        <div class="reservation-inner">
            <div class="menu_icon close-btn change" onclick="hideReservation(this)">
                <div class="bar1"></div>
                <div class="bar2"></div>
                <div class="bar3"></div>
            </div>
            <div class="content">
                <h3>MAKE A RESERVATION</h3>
                <p>Select Dining Details to Book</p>
                <div class="reservation-data">
                    <div class="left">
                        <select class="reservation-time_select" name="sele" id="reservation-time">
                            <option value="Launch">Launch</option>
                            <option value="Dinner">Dinner</option>
                        </select>
                        <span class="reservation-data-item" onclick="openReserTime(this)"><i class="far fa-clock"></i>
                            <span class="label">Time</span>
                            <span class="icon"></span>
                        </span>
                        <ul class="reservation-data-list reservation-time">
                            <li onclick="selectData(this)">Launch</li>
                            <li onclick="selectData(this)">Dinner</li>
                        </ul>
                    </div>
                    <div class="right">
                        <select class="reservation-person_select" name="" id="reservation-people">
                            <option value="Launch">Launch</option>
                            <option value="Dinner">Dinner</option>
                        </select>
                        <span class="reservation-data-item" onclick="openReserTime(this)"><i class="fas fa-user"></i>
                            <span class="label">People</span>
                            <span class="icon"></span>
                        </span>
                        <ul class="reservation-data-list reservation-people">
                            <li onclick="selectData(this)">1 People</li>
                            <li onclick="selectData(this)">2 People</li>
                        </ul>
                    </div>
                    <div class="date">
                        <label for="reservation_data">Select Date</label>
                        <div class="input-group date" data-provide="datepicker">
                            <input id="reservation_data" type="text" class="form-control datepicker" readonly>
                            <div class="input-group-addon">
                                <span class="glyphicon glyphicon-th"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="bottom-content">
                <button id="reservation_policy">Reservation Policy</button>
                <p>For bookings within a 24-hour period, please contact reservations directly on 02 9251 5600.</p>

                <p>Bookings are released on a six-monthly basis on the first day of each month (unless the first of the month falls on a day outside of office hours).</p>
                    
                <p>Tables for two are in high demand and are usually the first to book out. Tables for four are more readily available. If your requested date is unavailable, gather friends or family for a table of four.</p>
                    
                <p>Guests can certainly request tables but unfortunately, we cannot guarantee as we allocate on the day according to table configurations and booking date.</p>
                    
                <p>If you have any dietary requirements, please mention these when making your reservation.</p>
                    
                <p>Please view our reservation policy ahead of your visit to rainforest.com.</p>
            </div>
        </div>
    </div>
    <!-- Reservation END -->

    <!-- Content -->
    <div class="content-wrapper">
        <section class="section clear">
            <div class="right">
                <img src="images/big-thumb.jpg" class="section-img" alt="">
            </div>
            <div class="left section-text">
                <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Quasi suscipit sequi incidunt quaerat rerum modi praesentium alias
                    voluptatum earum, consequatur laboriosam odio a ipsa error laborum mollitia esse optio exercitationem maiores. Reiciendis</p>
                <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Quasi suscipit sequi incidunt quaerat rerum modi praesentium alias
                    voluptatum earum, consequatur laboriosam odio a ipsa error laborum mollitia esse optio exercitationem maiores.
                </p>
                <div class="text-link">
                    <a href="#" class="rain-btn-2">About Rain</a>
                    <a href="#" class="rain-btn-2">About Forest</a>
                </div>
            </div>
        </section>
    </div>

    <!-- Content END -->

    <!-- Subscribe -->
    <div class="subscribe-wrapper">
        <div class="inner">
            <div class="row">
                <div class="col-md-8">
                    <h2>Connect with us</h2>
                    <p>Sign up to be the first to receive special news and event updates from Quay.</p>
                </div>
                <div class="col-md-4">
                    <form action="">
                        <input type="text" class="email">
                        <input type="submit" class="submit-btn rain-btn" value="Subscribe">
                    </form>
                </div>
            </div>
        </div>
    </div>
    <!-- Subscribe END -->

    <!-- Footer -->
    <footer class="footer-wrapper">
        <div class="inner">
            <div class="row">
                <div class="col-md-4 footer-item">
                    <div class="footer-logo">
                        <img src="images/footer-logo.png" alt="">
                    </div>
                </div>
                <div class="col-md-4 footer-item">
                    <h3>Opening Hours</h3>
                    <div class="item-content">
                        <p>
                            <strong>LUNCH SERVICE</strong>
                            <br> Friday, Saturday and Sunday: Bookings from 12pm – 1.30pm
                        </p>
                        <p>
                            <strong>DINNER SERVICE</strong>
                            <br> Friday, Saturday and Sunday: Bookings from 12pm – 1.30pm
                        </p>
                        <p>
                            <strong>FUNCTIONS & EVENTS</strong>
                            <br> Friday, Saturday and Sunday: Bookings from 12pm – 1.30pm
                        </p>
                    </div>
                </div>
                <div class="col-md-4 footer-item">
                    <h3>Location</h3>
                    <p>Upper Level, Overseas Passenger Terminal, The Rocks, Sydney 2000 phone: 02 9251 5600</p>
                </div>
            </div>
            <div class="bottom">
                <div class="row">
                    <div class="col-md-6 col-sm-6 copyright">
                        <p><a href="#">Rain Forest</a> 2018. All Rights Reserved</p>
                    </div>
                    <div class="col-md-6 col-sm-6 pull-right social">
                        <ul>
                            <li>
                                <a href="#">
                                    <img src="images/social/iconmonstr-facebook-4-240.png" alt="">
                                </a>
                            </li>
                            <li>
                                <a href="#">
                                    <img src="images/social/iconmonstr-twitter-4-240.png" alt="">
                                </a>
                            </li>
                            <li>
                                <a href="#">
                                    <img src="images/social/iconmonstr-google-plus-4-240.png" alt="">
                                </a>
                            </li>
                            <li>
                                <a href="#">
                                    <img src="images/social/iconmonstr-youtube-9-240.png" alt="">
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </footer>
    <!-- Footer END -->

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.0/jquery.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"></script>
    <script src="js/jquery.datetimepicker.full.min.js"></script>
    <script src="js/jquery.scrollify.js"></script>
    <script src="js/custom.js"></script>

    <script>
        // number of drops created.
        var nbDrop = 50; 

        // function to generate a random number range.
        function randRange( minNum, maxNum) {
        return (Math.floor(Math.random() * (maxNum - minNum + 1)) + minNum);
        }

        // function to generate drops
        function createRain() {

            for( i=1;i<nbDrop;i++) {
            var dropLeft = randRange(0,1600);
            var dropTop = randRange(-1000,1400);

            $('.rain').append('<div class="drop" id="drop'+i+'"></div>');
            $('#drop'+i).css('left',dropLeft);
            $('#drop'+i).css('top',dropTop);
            }

        }
        // Make it rain
        createRain();
    </script>
</body>
</html>