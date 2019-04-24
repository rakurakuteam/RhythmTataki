@extends('master')

@section('title')
    주문페이지
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/store/order.css')}}" />
@endsection

@section('header')
    @include('layouts.header.store')
@endsection

@section('nav')
    
@endsection

@section('body')
    @include('components.store.order')
@endsection

@section('footer')
    @include('layouts.footer.footer')
@endsection

@push('scripts')
    <script src="{{asset('js/store/quantity.js')}}"></script>
    <script src="{{asset('js/store/order.js')}}"></script>
    <script src="http://dmaps.daum.net/map_js_init/postcode.v2.js"></script>
@endpush