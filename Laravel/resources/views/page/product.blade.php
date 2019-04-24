@extends('master')

@section('title')
    상품상세
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/store/storeDetail.css')}}" />
@endsection

@section('header')
    @include('layouts.header.store')
@endsection

@section('nav')

@endsection

@section('body')
    @include('components.store.product')
@endsection

@section('footer')
    @include('components.store.footer')
@endsection

@push('scripts')
    <script src="{{asset('js/store/cart.js')}}"></script>
    <script src="{{asset('js/store/quantity.js')}}"></script>
@endpush