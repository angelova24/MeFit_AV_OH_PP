<script setup>
    import { useStore } from 'vuex';

    import ProgramListItem from './ProgramListItem.vue';

    const store = useStore();

    const props = defineProps({
        programs: {
            type: Array,
            required: true
        },
        header: {
            type: String,
            required:false
        }
    });
    const emits = defineEmits(["programListItemClicked"]);
    const onProgramListItemClicked = (event, id) => {
        console.log(`ProgramListItem with id ${id} was clicked...`)
        store.commit("setProgramDetailsId", id);
        emits("programListItemClicked");
    }

</script>

<template>
    <div>
        <header>
            <b>{{ header }}</b>
        </header>
        <main>
            <ProgramListItem 
                v-for="program in programs" 
                v-bind:program="program"
                v-bind:key="program.id"
                v-on:click="onProgramListItemClicked($event, program.id)">
            </ProgramListItem>
        </main>
    </div>
</template>

<style scoped>

</style>
