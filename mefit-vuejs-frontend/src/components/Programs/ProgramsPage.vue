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
            Everything about MeFit fitness programs...
        </header>
        <main>
            <section v-if="JSON.stringify(programs) !== '[]'" title="programList">
                <ProgramList 
                    v-bind:programs="programs"
                    header="Click on any of these programs to view details..."
                ></ProgramList>
            </section>
            <section v-if="program !== undefined" title="programDetails">
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
        padding: 50px 10px;
    }
    main {
        display: grid;
        grid-template-columns: 1fr 1fr;
        row-gap: 10px;
        column-gap: 10px;        
    }
    section {
        border: 1px groove;
        border-color: lightseagreen;
        box-shadow: 2px 2px 2px 1px grey;
    }

</style>
