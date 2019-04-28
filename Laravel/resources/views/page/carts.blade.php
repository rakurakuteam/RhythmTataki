@extends('master')

@section('title')
    장바구니
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/store/storeCarts.css')}}" />
@endsection

@section('header')
    @include('layouts.header.store')
@endsection

@section('nav')

@endsection

@section('body')
    @include('components.store.carts')
@endsection

@section('footer')
    @include('layouts.footer.footer')
@endsection

@push('scripts')
    <script src="{{asset('js/store/quantity.js')}}"></script>
    <script src="{{asset('js/store/cart.js')}}"></script>
@endpush
