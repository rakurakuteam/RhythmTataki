<template>
    <v-container grid-list-md="grid-list-md" text-xs-center="text-xs-center">
        <globalNav></globalNav>
        <router-view></router-view>
    </v-container>
</template>

<script>
    export default {
        computed: {
            binding() {
                const binding = {}

                if (this.$vuetify.breakpoint.mdAndUp) 
                    binding.column = true

                return binding
            }
        }
    }
</script>
