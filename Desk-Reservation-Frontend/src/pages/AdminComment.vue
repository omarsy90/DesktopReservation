<template>
  <div>
    <TwoColumnLayout>
      <AdminCommentSection />
    </TwoColumnLayout>
  </div>
</template>

<script>
import AdminCommentSection from "../components/AdminCommentSection.vue";
import TwoColumnLayout from "../components/TwoColumnLayout.vue";

export default {
  components: {
    AdminCommentSection,
    TwoColumnLayout,
  },
};
</script>


