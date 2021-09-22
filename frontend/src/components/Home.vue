<template>
  <div class="container">
    <div class="jumbotron">
      <h1 class="display-4">Url shortener</h1>
      <b-form @submit="onSubmit" @reset="onReset">
        <b-form-group>
          <b-form-input
            v-model="form.url"
            type="text"
            placeholder="Enter URL"
            :readonly="!!shortenedUrl"
            required
          ></b-form-input>
        </b-form-group>

        <b-button
          v-if="!shortenedUrl"
          type="submit"
          variant="primary"
          size="lg"
          class="my-2"
          >Shorten</b-button
        >
        <div v-if="!!shortenedUrl">
          <b-form-group label="Your shortened url">
            <b-form-input
              v-model="shortenedUrl"
              type="text"
              readonly
            ></b-form-input>
          </b-form-group>
          <b-button type="reset" variant="primary" size="lg" class="my-2"
            >Shorten another url</b-button
          >
        </div>
      </b-form>
    </div>
  </div>
</template>

<script>
import { config } from "@/config.js";

export default {
  name: "Home",
  data() {
    return {
      form: {
        url: null
      },
      shortenedUrl: null
    };
  },
  methods: {
    onSubmit(event) {
      event.preventDefault();
      this.$http
        .post(`${config.SHORTNER_HOSTNAME}?url=${this.form.url}`)
        .then(
          response =>
            (this.shortenedUrl = config.SHORTNER_HOSTNAME + response.data)
        )
        .catch(error => console.log(error));
    },
    onReset(event) {
      event.preventDefault();
      // Reset our form values
      this.form.url = "";
      // Trick to reset/clear native browser form validation state
      (this.shortenedUrl = null),
        this.$nextTick(() => {
          this.show = true;
        });
    }
  }
};
</script>
