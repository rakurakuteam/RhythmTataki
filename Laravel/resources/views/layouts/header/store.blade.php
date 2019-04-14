<!--상단바-->
<center>
    <nav id="topMenu">
        <div id="logoBox">
            <a href="{{route('home')}}">
                <img src="{{asset('images/pic/rythmStore5.png')}}" id="logo"/>
            </a>
        </div>
        <ul>
            <li>
                <a class="menuLink" id="li-1" href="#">
                    <strong>{{__('messages.order')}}
                        /
                        {{__('messages.delivery')}}</strong>
                </a>
            </li>
            <li>
                <a class="menuLink" href="#">
                    <strong>{{__('messages.cart')}}</strong>
                </a>
            </li>
            <li>
                <a class="menuLink" href="#">
                    <strong>{{__('messages.login')}}</strong>
                </a>
            </li>
        </ul>
    </nav>
</center>