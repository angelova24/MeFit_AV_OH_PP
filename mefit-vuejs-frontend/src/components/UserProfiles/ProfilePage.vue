<script setup>
    import { computed } from "vue";
    import { useStore } from "vuex";

    const store = useStore();
    const profile = computed(() => store.state.profile);
</script>

<template>
    <div>
        <header>
            <h1> Profile Page </h1>
        </header>
        <main>
            <section title="fitnessEvaluation" >
                <span>Your current fitness evaluation:</span>
                <label for="weight">
                    Weight:
                </label>
                <input id="weight" type="text" v-model="profile.weight" />
                <label for="height">
                    Height:
                </label>
                <input id="height" type="text" v-model="profile.height" />
                <label for="medicalCondition">
                    Medical condition:
                </label>
                <input id="medicalCondition" type="text" v-model="profile.medicalCondition" />
                <label for="disabilities">
                    Disabilities:
                </label>
                <input id="disabilities" type="text" v-model="profile.disabilities" />
            </section>
            <section title="addressData" >
                <span>Your address data:</span>
                <label for="addressLine1">
                    Address line 1:
                </label>
                <input id="addressLine1" type="text" v-model="profile.addressLine1" />
                <label for="addressLine2">
                    Address line 2:
                </label>
                <input id="addressLine2" type="text" v-model="profile.addressLine2" />
                <label for="postalCode">
                    Postal code:
                </label>
                <input id="postalCode" type="text" v-model="profile.postalCode" />
                <label for="city">
                    City:
                </label>
                <input id="city" type="text" v-model="profile.city" />
                <label for="country">
                    Country:
                </label>
                <input id="country" type="text" v-model="profile.country" />
            </section>
            <button v-on:click="updateProfile" type="submit">Save changes</button>
        </main>
    </div>
</template>

<style scoped>
    section {
        display: flex;
        flex-wrap: wrap;
        row-gap: 10px;
        column-gap: 10px;
        align-items:stretch;
        margin-bottom: 20px;
    }
    label {
        flex: 1 1 45%;  /*grow | shrink | basis */
        border: 1px dashed;
        border-color: blue;
    }
    input {
        flex: 1 1 45%;  /*grow | shrink | basis */
        border: 1px dashed;
        border-color: red;
    }
    span {
        flex: 1 1 95%;  /*grow | shrink | basis */
        border: 1px dashed;
        border-color: green;
        text-align: left;
    }
    header {
        margin-bottom: 20px;
    }
</style>
