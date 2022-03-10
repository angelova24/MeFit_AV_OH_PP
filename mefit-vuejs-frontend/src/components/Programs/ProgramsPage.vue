<script setup>
    import { useStore } from 'vuex';
    import { computed } from 'vue';
    import ProgramList from './ProgramList.vue';
    import ProgramDetails from './ProgramDetails.vue';  

    const store = useStore();

    //--- get all Programs from Store
    const programs = computed(() => store.state.programs);
    console.log(JSON.stringify(programs.value));
    //--- get id of selected program
    const programDetailsId = computed(() => store.state.programDetailsId);
    console.log(`selected program id: ${programDetailsId.value}`);
    //--- get selected program
    const program = computed(() => store.getters.getProgramById(programDetailsId.value));
    console.log(`selected program: ${program.value}`);

</script>


<template>
    <div>
        <header>
            Program Page
        </header>
        <main>
            <section v-if="JSON.stringify(programs) !== '[]'" title="programList">
                <ProgramList v-bind:programs="programs"></ProgramList>
            </section>
            <section v-if="JSON.stringify(programs) !== '[]'" title="programDetails">
                Details of program:
                <ProgramDetails v-bind:program="program"></ProgramDetails>
            </section>
            <section v-if="JSON.stringify(programs) === '[]'">
                We are sorry, but currently no programs are available.<br />
                Please try again later...
            </section>
        </main>
    </div>
</template>

<style scoped>
    header {
        font-size: larger;
    }
    main {
        display: flex;
        flex-wrap: wrap;
        row-gap: 10px;
        column-gap: 10px;
        align-items:stretch;
        
    }
    section {
        flex: 1 1 45%;  /*grow | shrink | basis */
        border: 2px dashed;
        border-color: red;
    }
    img {
        width: 300px;
    }
</style>
