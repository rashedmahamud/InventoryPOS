<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!-- Content -->
    <div class="content-wrapper">
        <section class="section clear">
            <div class="right theme-slider">
                <!-- <img src="images/IMG_1516.JPG" class="section-img" alt=""> -->
                <div id="demo" class="carousel slide" data-ride="carousel">
                    <ul class="carousel-indicators">
                        <li data-target="#demo" data-slide-to="0" class="active"></li>
                        <li data-target="#demo" data-slide-to="1"></li>
                    </ul>
                    <div class="carousel-inner">
                        <div class="carousel-item active">
                        <img src="images/IMG_1516.JPG" alt="Los Angeles" width="1100" height="500">
                        <div class="carousel-caption">
                            <h3>Slide 1</h3>
                            <p>We had such a great time!</p>
                        </div>   
                        </div>
                        <div class="carousel-item">
                        <img src="images/IMG_1515.JPG" alt="Chicago" width="1100" height="500">
                        <div class="carousel-caption">
                            <h3>Slide 2</h3>
                            <p>Thank you!</p>
                        </div>   
                        </div>
                    </div>
                    <a class="carousel-control-prev" href="#demo" data-slide="prev">
                        <span class="carousel-control-prev-icon"></span>
                    </a>
                    <a class="carousel-control-next" href="#demo" data-slide="next">
                        <span class="carousel-control-next-icon"></span>
                    </a>
                    </div>
                </div>
            <div class="left section-text">
                <h2>PETER GILMORE</h2>
                <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Quasi suscipit sequi incidunt quaerat rerum modi praesentium alias
                    voluptatum earum, consequatur laboriosam odio a ipsa error laborum mollitia esse optio exercitationem maiores. Reiciendis</p>
                <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Quasi suscipit sequi incidunt quaerat rerum modi praesentium alias
                    voluptatum earum, consequatur laboriosam odio a ipsa error laborum mollitia esse optio exercitationem maiores.
                </p>
                <a href="#" class="rain-btn">Continue Reading</a>
            </div>
        </section>

        <section class="section clear">
            <div class="left">
                <img src="images/IMG_1515.JPG" class="section-img" alt="">
            </div>
            <div class="right section-text">
                <h2>PETER GILMORE</h2>
                <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Quasi suscipit sequi incidunt quaerat rerum modi praesentium alias voluptatum earum, consequatur laboriosam odio a ipsa error laborum mollitia esse optio exercitationem maiores. 
                </p>
                <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Quasi suscipit sequi incidunt quaerat rerum modi praesentium alias voluptatum earum, consequatur laboriosam odio a ipsa error laborum mollitia esse optio exercitationem maiores.
                </p>
                <a href="#" class="rain-btn">Continue Reading</a>
            </div>
        </section>

        <section class="section clear last-sec">
            <div class="right">
                <img src="images/IMAG4311.jpg" class="section-img" alt="">
            </div>
            <div class="left section-text">
                <h2>PETER GILMORE</h2>
                <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Quasi suscipit sequi incidunt quaerat rerum modi praesentium
                    alias voluptatum earum, consequatur laboriosam odio a ipsa error laborum mollitia esse optio exercitationem maiores.
                    Reiciendis nam quam itaque accusantium doloremque in iure quae minima, minus numquam natus, tempora, rem saepe
                    veritatis! Magnam, odit.
                </p>
                <a href="#" class="rain-btn">Continue Reading</a>
            </div>
        </section>

        <!-- Grid 2 -->
        <div class="grid-2-style">
            <div class="container-fluid">
                <div class="row inner-row">
                    <div class="col-md-4 grid-item">
                        <div class="grid-item-inner">
                            <div class="title">
                                <h3>Title</h3>
                            </div>
                            <div class="grid-text">
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto ullam consequuntur assumenda vel nihil eveniet
                                    aperiam. Dicta adipisci ex quaerat.</p>
                            </div>
                            <div class="grid-btn">
                                <a href="#">Read More</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 grid-item">
                        <div class="grid-item-inner">
                            <div class="title">
                                <h3>Title</h3>
                            </div>
                            <div class="grid-text">
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto ullam consequuntur assumenda vel nihil eveniet
                                    aperiam. Dicta adipisci ex quaerat.</p>
                            </div>
                            <div class="grid-btn">
                                <a href="#">Read More</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 grid-item">
                        <div class="grid-item-inner">
                            <div class="title">
                                <h3>Title</h3>
                            </div>
                            <div class="grid-text">
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto ullam consequuntur assumenda vel nihil eveniet
                                    aperiam. Dicta adipisci ex quaerat.</p>
                            </div>
                            <div class="grid-btn">
                                <a href="#">Read More</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row inner-row">
                    <div class="col-md-4 grid-item">
                        <div class="grid-item-inner">
                            <div class="title">
                                <h3>Title</h3>
                            </div>
                            <div class="grid-text">
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto ullam consequuntur assumenda vel nihil eveniet
                                    aperiam. Dicta adipisci ex quaerat.</p>
                            </div>
                            <div class="grid-btn">
                                <a href="#">Read More</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 grid-item">
                        <div class="grid-item-inner">
                            <div class="title">
                                <h3>Title</h3>
                            </div>
                            <div class="grid-text">
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto ullam consequuntur assumenda vel nihil eveniet
                                    aperiam. Dicta adipisci ex quaerat.</p>
                            </div>
                            <div class="grid-btn">
                                <a href="#">Read More</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 grid-item">
                        <div class="grid-item-inner">
                            <div class="title">
                                <h3>Title</h3>
                            </div>
                            <div class="grid-text">
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto ullam consequuntur assumenda vel nihil eveniet aperiam.
                                    Dicta adipisci ex quaerat.</p>
                            </div>
                            <div class="grid-btn">
                                <a href="#">Read More</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Grid 2 End -->
        <!-- Grid 3 -->
        <div class="grid-3-style">
            <div class="container-fluid">
                <div class="row inner-row">
                    <div class="col-md-3 grid-item">
                        <div class="grid-item-inner">
                            <div class="title">
                                <h3>Title</h3>
                            </div>
                            <div class="grid-text">
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto ullam consequuntur assumenda vel nihil
                                    eveniet aperiam. Dicta adipisci ex quaerat.</p>
                            </div>
                            <div class="grid-btn">
                                <a href="#">Read More</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 grid-item">
                        <div class="grid-item-inner">
                            <div class="title">
                                <h3>Title</h3>
                            </div>
                            <div class="grid-text">
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto ullam consequuntur assumenda vel nihil
                                    eveniet aperiam. Dicta adipisci ex quaerat.</p>
                            </div>
                            <div class="grid-btn">
                                <a href="#">Read More</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 grid-item">
                        <div class="grid-item-inner">
                            <div class="title">
                                <h3>Title</h3>
                            </div>
                            <div class="grid-text">
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto ullam consequuntur assumenda vel nihil
                                    eveniet aperiam. Dicta adipisci ex quaerat.</p>
                            </div>
                            <div class="grid-btn">
                                <a href="#">Read More</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row inner-row">
                    <div class="col-md-3 grid-item">
                        <div class="grid-item-inner">
                            <div class="title">
                                <h3>Title</h3>
                            </div>
                            <div class="grid-text">
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto ullam consequuntur assumenda vel nihil
                                    eveniet aperiam. Dicta adipisci ex quaerat.</p>
                            </div>
                            <div class="grid-btn">
                                <a href="#">Read More</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 grid-item">
                        <div class="grid-item-inner">
                            <div class="title">
                                <h3>Title</h3>
                            </div>
                            <div class="grid-text">
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto ullam consequuntur assumenda vel nihil
                                    eveniet aperiam. Dicta adipisci ex quaerat.</p>
                            </div>
                            <div class="grid-btn">
                                <a href="#">Read More</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 grid-item">
                        <div class="grid-item-inner">
                            <div class="title">
                                <h3>Title</h3>
                            </div>
                            <div class="grid-text">
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto ullam consequuntur assumenda vel nihil
                                    eveniet aperiam. Dicta adipisci ex quaerat.</p>
                            </div>
                            <div class="grid-btn">
                                <a href="#">Read More</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Grid 3 End -->

        <!-- Grid 4 -->
        <div class="grid-4-style">
            <div class="container-fluid">
                <div class="row inner-row">
                    <div class="col-md-8 grid-item">
                        <div class="grid-item-inner">
                            <div class="title">
                                <h3>Title</h3>
                            </div>
                            <div class="grid-text">
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto ullam consequuntur assumenda vel nihil
                                    eveniet aperiam. Dicta adipisci ex quaerat.</p>
                            </div>
                            <div class="grid-btn">
                                <a href="#">Read More</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-12 grid-item">
                                <div class="grid-item-inner">
                                    <div class="title">
                                        <h3>Title</h3>
                                    </div>
                                    <div class="grid-text">
                                        <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto ullam consequuntur assumenda vel nihil eveniet aperiam.
                                            Dicta adipisci ex quaerat.</p>
                                    </div>
                                    <div class="grid-btn">
                                        <a href="#">Read More</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 grid-item">
                                <div class="grid-item-inner">
                                    <div class="title">
                                        <h3>Title</h3>
                                    </div>
                                    <div class="grid-text">
                                        <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto ullam consequuntur assumenda vel nihil eveniet
                                            aperiam. Dicta adipisci ex quaerat.</p>
                                    </div>
                                    <div class="grid-btn">
                                        <a href="#">Read More</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Grid 4 End -->
    </div>

</asp:Content>

