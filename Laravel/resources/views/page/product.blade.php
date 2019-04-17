@extends('master')

@section('title')
    상품상세
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="shopDetail.css" />
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

@endsection

@push('scripts')
    <script src="{{asset('js/store/cart.js')}}"></script>
@endpush